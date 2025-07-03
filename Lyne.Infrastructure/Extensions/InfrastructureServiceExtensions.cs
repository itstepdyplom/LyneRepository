using Lyne.Domain.IRepositories;
using Lyne.Infrastructure.Persistence;
using Lyne.Infrastructure.Repositories;
using Lyne.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lyne.Infrastructure.Extensions;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
    //repository service, Redis, RabbitMQ etc.
}
