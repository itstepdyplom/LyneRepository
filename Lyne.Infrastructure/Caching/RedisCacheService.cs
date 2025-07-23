using System.Text.Json;
using StackExchange.Redis;

namespace Lyne.Infrastructure.Caching;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _db;
    private readonly IConnectionMultiplexer _redis;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _redis = redis;
        _db = redis.GetDatabase();
    }

    public async Task SetAsync<T>(string key, T value, string prefix, TimeSpan? expiry = null)
    {
        var json = JsonSerializer.Serialize(value);
        await _db.StringSetAsync(key, json, expiry);
        await _db.SetAddAsync($"{prefix}_keys", key);
    }

    public async Task RemoveAsync(string key, string prefix)
    {
        await _db.KeyDeleteAsync(key);
        await _db.SetRemoveAsync($"{prefix}_keys", key);
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var json = await _db.StringGetAsync(key);
        return json.HasValue ? JsonSerializer.Deserialize<T>(json!) : default;
    }

    public async Task<List<T>> GetAllAsync<T>(string prefix)
    {
        var keys = await _db.SetMembersAsync($"{prefix}_keys");
        var result = new List<T>();
    
        foreach (var redisKey in keys)
        {
            var value = await _db.StringGetAsync(redisKey.ToString());
            if (value.HasValue)
                result.Add(JsonSerializer.Deserialize<T>(value!)!);
        }

        return result;
    }
}