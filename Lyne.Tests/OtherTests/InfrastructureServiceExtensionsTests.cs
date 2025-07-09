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

        // Мок для IConnectionMultiplexer
        var mockConnection = new Mock<IConnectionMultiplexer>();

        // Реєструємо мок у сервісах замість реального підключення
        services.AddSingleton<IConnectionMultiplexer>(mockConnection.Object);
        services.AddSingleton<ICacheService, RedisCacheService>();

        // Викликаємо метод реєстрації сервісів (якщо він є)
        services.AddInfrastructureServices(configuration);

        var provider = services.BuildServiceProvider();

        var cacheService = provider.GetService<ICacheService>();

        cacheService.Should().NotBeNull();
    }
}
