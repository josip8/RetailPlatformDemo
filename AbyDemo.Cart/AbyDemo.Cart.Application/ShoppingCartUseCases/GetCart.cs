using AbyDemo.Cart.Application.ShoppingCartUseCases.Contracts;
using AbyDemo.Cart.Domain.Contracts.Cache;
using AbyDemo.Cart.Domain.Contracts.Repositories;
using AbyDemo.Cart.Domain.Entities;

namespace AbyDemo.Cart.Application.ShoppingCartUseCases;

public class GetCart(ICartRepository repository, ICacheService cache) : IGetCart
{
    private readonly ICartRepository _repository = repository;
    private readonly ICacheService _cache = cache;

    public async Task<ShoppingCart> Execute(string userId)
    {
        try
        {
            var cachedCart = await _cache.Get(userId);
            if (cachedCart != null)
            {
                return cachedCart;
            }
        }
        catch
        {
            // Log cache retrieval failure if necessary
        }


        var cart = await _repository.GetCart(userId);
        cart ??= new ShoppingCart { UserId = userId };

        await _cache.Set(userId, cart);
        return cart;
    }
}
