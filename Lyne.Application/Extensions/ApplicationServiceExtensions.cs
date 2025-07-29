using Lyne.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace Lyne.Application.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        return services;
    }
}
