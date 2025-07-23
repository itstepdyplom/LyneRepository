using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lyne.Tests.OtherTests;

public class SwaggerGenOptionsExtensionsTests
{
    [Fact]
    public void ConfigureSwagger_SetsExpectedDocument()
    {
        var options = new SwaggerGenOptions();
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Test API", Version = "v1" });

        var swaggerDocs = options.SwaggerGeneratorOptions.SwaggerDocs;

        Assert.NotNull(swaggerDocs);
        Assert.Contains(swaggerDocs, d => d.Key == "v1" && d.Value.Title == "Test API" && d.Value.Version == "v1");
    }
}