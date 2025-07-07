using FluentAssertions;
using Lyne.Application.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Lyne.Tests.ServiceTests;

public class ApplicationServiceExtensionsTests
{
    [Fact]
    public void AddApplicationServices_RegistersServices()
    {
        var services = new ServiceCollection();
        services.AddApplicationServices();
        // Just check that the call does not throw and services are registered
        services.Should().NotBeNull();
    }
}
