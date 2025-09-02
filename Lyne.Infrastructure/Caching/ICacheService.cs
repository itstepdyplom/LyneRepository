namespace Lyne.Infrastructure.Caching;

public interface ICacheService
{
    Task<List<T?>> GetAllAsync<T>(string prefix);
    Task<T?> GetAsync<T>(string key);
    public Task SetAsync<T>(string key, T value, string prefix, TimeSpan? expiry = null);
    public Task RemoveAsync(string key, string prefix);
}
