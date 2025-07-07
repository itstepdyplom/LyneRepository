using FluentAssertions;
using Lyne.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lyne.Tests.OtherTests;

public class InfrastructureServiceExtensionsTests
{
    [Fact]
    public void AddInfrastructureServices_RegistersServices()
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();
        services.AddInfrastructureServices(configuration);
        services.Should().NotBeNull();
    }
}
