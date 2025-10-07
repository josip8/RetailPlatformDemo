using AbyDemo.Cart.Application.ShoppingCartUseCases;
using AbyDemo.Cart.Application.ShoppingCartUseCases.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace AbyDemo.Cart.Application.Config;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IGetCart, GetCart>();
        services.AddScoped<IUpsertCartItem, UpsertCartItem>();
        services.AddScoped<IRemoveCartItem, RemoveCartItem>();
        services.AddScoped<IClearCart, ClearCart>();
        return services;
    }
}
