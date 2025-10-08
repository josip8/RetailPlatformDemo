using AbyDemo.Cart.Application.Products;
using AbyDemo.Cart.Domain.Contracts.Cache;
using AbyDemo.Cart.Domain.Contracts.Events;
using AbyDemo.Cart.Domain.Contracts.Repositories;
using AbyDemo.Cart.Infrastructure.Cache;
using AbyDemo.Cart.Infrastructure.Data;
using AbyDemo.Cart.Infrastructure.Data.Repositories;
using AbyDemo.Cart.Infrastructure.Products;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AbyDemo.Cart.Infrastructure.Config;

public static class DependencyInjection
{
    public static IServiceCollection AddRedisCache(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
        });

        services.AddScoped<ICacheService, RedisCacheService>();
        return services;
    }

    public static IServiceCollection AddMongoDatabase(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        CartDbContext.Configure();
        var mongoConnectionString = configuration.GetConnectionString("Mongo");
        var mongoClient = new MongoDB.Driver.MongoClient(mongoConnectionString);
        var mongoDatabase = mongoClient.GetDatabase("cart_database");
        services.AddSingleton(mongoDatabase);
        services.AddSingleton<ICartRepository, MongoCartRepository>();
        return services;
    }

    public static IServiceCollection AddProductService(this IServiceCollection services)
    {
        services.AddHttpClient<IProductService, ProductsHttpClient>(client =>
        {
            client.BaseAddress = new Uri("https://fake-url.com/");
        });

        return services;
    }

    public static IServiceCollection AddEventPublisher(this IServiceCollection services)
    {
        services.AddSingleton<IEventPublisher, KafkaEventPublisher>();
        return services;
    }
}
