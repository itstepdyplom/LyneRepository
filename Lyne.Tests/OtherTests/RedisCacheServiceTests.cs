using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Lyne.Application.DTO;
using Lyne.Infrastructure.Caching;
using Microsoft.Extensions.Logging;
using Moq;
using StackExchange.Redis;
using Xunit;

public class TestDto
{
    public string Name { get; set; }
}

public class RedisCacheServiceTests
{
    private readonly Mock<IConnectionMultiplexer> _mockMultiplexer;
    private readonly Mock<IDatabase> _mockDatabase;
    private readonly RedisCacheService _service;

    public RedisCacheServiceTests()
    {
        _mockMultiplexer = new Mock<IConnectionMultiplexer>();
        _mockDatabase = new Mock<IDatabase>();

        _mockMultiplexer.Setup(m => m.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
            .Returns(_mockDatabase.Object);

        _service = new RedisCacheService(_mockMultiplexer.Object);
    }

    [Fact]
    public async Task SetAsync_ShouldStoreValue()
    {
        var key = "test:key";
        var value = new { Name = "Test" };
        _mockDatabase.Setup(db => db.StringSetAsync(key, It.IsAny<RedisValue>(), null, false, When.Always, CommandFlags.None))
            .ReturnsAsync(true);

        await _service.SetAsync(key, value,"test");

        _mockDatabase.Verify(db => db.StringSetAsync(key, It.IsAny<RedisValue>(), null, false, When.Always, CommandFlags.None), Times.Once);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnDeserializedValue()
    {
        var key = "test:key";
        var obj = new TestDto { Name = "Test" };
        var json = JsonSerializer.Serialize(obj);
        _mockDatabase.Setup(db => db.StringGetAsync(key, CommandFlags.None))
            .ReturnsAsync(json);

        var result = await _service.GetAsync<TestDto>(key);

        Assert.Equal("Test", result?.Name);
    }

    [Fact]
    public async Task RemoveAsync_ShouldDeleteKey()
    {
        var key = "test:key";
        _mockDatabase.Setup(db => db.KeyDeleteAsync(key, CommandFlags.None))
            .ReturnsAsync(true);

        await _service.RemoveAsync(key,"test");

        _mockDatabase.Verify(db => db.KeyDeleteAsync(key, CommandFlags.None), Times.Once);
    }

    // For GetAllAsync, you need to mock IServer and Keys
  
    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfValues()
    {
        var prefix = "user";
        var keys = new RedisKey[] { "user:1", "user:2" };
        var values = new RedisValue[]
        {
            JsonSerializer.Serialize(new UserDto { Id = 1 }),
            JsonSerializer.Serialize(new UserDto { Id = 2 })
        };

        var mockServer = new Mock<IServer>();
        var endpoint = new DnsEndPoint("localhost", 6379);

        mockServer.Setup(s => s.Keys(It.IsAny<int>(), $"{prefix}:*", It.IsAny<int>(), It.IsAny<long>(), It.IsAny<int>(), It.IsAny<CommandFlags>()))
            .Returns(keys);

        _mockMultiplexer.Setup(m => m.GetEndPoints(It.IsAny<bool>())).Returns(new[] { endpoint });
        _mockMultiplexer.Setup(m => m.GetServer(endpoint, It.IsAny<object>())).Returns(mockServer.Object);

        _mockDatabase.Setup(db => db.StringGetAsync(keys, CommandFlags.None)).ReturnsAsync(values);
        _mockDatabase.SetupGet(db => db.Multiplexer).Returns(_mockMultiplexer.Object);

        var result = await _service.GetAllAsync<UserDto>(prefix);

        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0]?.Id);
        Assert.Equal(2, result[1]?.Id);
    }
}