using Lyne.Application.Services;
using Lyne.Domain.IRepositories;
using Lyne.Infrastructure.Caching;
using Lyne.Infrastructure.Persistence;
using Lyne.Infrastructure.Repositories;
using Lyne.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

namespace Lyne.Infrastructure.Extensions;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lyne API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter your valid token."
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
        
        // Register services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<AuthService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IJwtService, JwtService>();
        // Register repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        
        services.AddLogging();
        
        // C#
        var redisSection = configuration.GetSection("Redis");
        var host = redisSection["Host"];
        var port = int.Parse(redisSection["Port"] ?? "6379");
        var password = redisSection["Password"];
        var ssl = bool.Parse(redisSection["Ssl"] ?? "false");
        var user = redisSection["User"];

        var config = new ConfigurationOptions
        {
            EndPoints = { { host, port } },
            User = user,
            Password = password,
            Ssl = ssl,
            AbortOnConnectFail = false,
            ConnectTimeout = 10000,
            SyncTimeout = 10000,
        };

        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(config));
        services.AddSingleton<ICacheService, RedisCacheService>();
        
        return services;
    }
    //repository service, Redis, RabbitMQ etc.
}
