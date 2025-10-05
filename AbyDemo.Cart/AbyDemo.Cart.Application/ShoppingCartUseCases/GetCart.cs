using AbyDemo.Cart.Application.Contracts;
using AbyDemo.Cart.Application.ShoppingCartUseCases.Contracts;
using AbyDemo.Cart.Domain.Contracts.Repositories;
using AbyDemo.Cart.Domain.Entities;

namespace AbyDemo.Cart.Application.ShoppingCartUseCases;

public class GetCart : IGetCart
{
    private readonly ICartRepository _repository;
    private readonly ICacheService _cache;

    public GetCart(ICartRepository repository, ICacheService cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<ShoppingCart> Execute(string userId)
    {
        var cachedCart = await _cache.Get(userId);
        if (cachedCart != null)
        {
            return cachedCart;
        }

        var cart = await _repository.GetCartAsync(userId);
        cart ??= new ShoppingCart { UserId = userId };

        await _cache.Set(userId, cart);
        return cart;
    }
}
