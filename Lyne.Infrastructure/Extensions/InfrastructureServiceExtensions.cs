using Lyne.Application.Services;
using Lyne.Domain.IRepositories;
using Lyne.Infrastructure.Caching;
using Lyne.Infrastructure.Persistence;
using Lyne.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Lyne.Infrastructure.Extensions;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Register repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IJwtService, JwtService>();

        
        // C#
        var redisConfig = configuration.GetConnectionString("Redis");
        if (string.IsNullOrEmpty(redisConfig))
            throw new ArgumentNullException(nameof(redisConfig), "Redis connection string is missing.");

        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConfig));
        services.AddSingleton<ICacheService, RedisCacheService>();
        
        return services;
    }
    //repository service, Redis, RabbitMQ etc.
}
