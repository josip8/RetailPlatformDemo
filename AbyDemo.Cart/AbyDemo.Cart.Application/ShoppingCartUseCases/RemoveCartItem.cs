using AbyDemo.Cart.Application.ShoppingCartUseCases.Contracts;
using AbyDemo.Cart.Domain.Contracts.Cache;
using AbyDemo.Cart.Domain.Contracts.Repositories;
using AbyDemo.Cart.Domain.Entities;

namespace AbyDemo.Cart.Application.ShoppingCartUseCases;

internal class RemoveCartItem(ICartRepository repository, ICacheService cache, IGetCart getCart)
    : IRemoveCartItem
{
    private readonly ICartRepository _repository = repository;
    private readonly ICacheService _cache = cache;
    private readonly IGetCart _getCart = getCart;

    public async Task<ShoppingCart> Execute(string userId, Guid productId)
    {
        var cart = await _getCart.Execute(userId);
        var cartItem = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);

        if (cartItem == null)
        {
            return cart;
        }

        cart.CartItems.Remove(cartItem);
        await _cache.Set(userId, cart);
        var savedCart = await _repository.SaveCart(cart);

        return savedCart;
    }
}
