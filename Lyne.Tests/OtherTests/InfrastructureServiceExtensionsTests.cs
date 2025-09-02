using FluentAssertions;
using Lyne.Infrastructure.Caching;
using Lyne.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StackExchange.Redis;

namespace Lyne.Tests.OtherTests;

public class InfrastructureServiceExtensionsTests
{
    [Fact]
    public void AddInfrastructureServices_RegistersServices()
    {
        var services = new ServiceCollection();

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                ["ConnectionStrings:Redis"] = "localhost:6379"
            }!)
            .Build();

        var mockConnection = new Mock<IConnectionMultiplexer>();

        services.AddSingleton<IConnectionMultiplexer>(mockConnection.Object);
        services.AddSingleton<ICacheService, RedisCacheService>();

        services.AddInfrastructureServices(configuration);

        var provider = services.BuildServiceProvider();

        var cacheService = provider.GetService<ICacheService>();

        cacheService.Should().NotBeNull();
    }
    
    [Fact]
    public void AddInfrastructureServices_RegistersExpectedServices()
    {
        var services = new ServiceCollection();
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                ["ConnectionStrings:Redis"] = "localhost:6379"
            }!)
            .Build();

        services.AddInfrastructureServices(config);

        Assert.Contains(services, s => s.ServiceType == typeof(ICacheService));
    }
}
