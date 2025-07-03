using Lyne.Application.Mapping;
using Lyne.Application.Services;
using Lyne.Domain.IRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace Lyne.Application.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //services.AddScoped<IUserService, UserService>();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<UserService>();
        services.AddScoped<AuthService>();

        return services;
    }
}
