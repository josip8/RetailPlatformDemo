using AbyDemo.Cart.Application.ShoppingCartUseCases.Contracts;
using AbyDemo.Cart.Domain.Contracts.Cache;
using AbyDemo.Cart.Domain.Contracts.Events;
using AbyDemo.Cart.Domain.Contracts.Repositories;
using AbyDemo.Cart.Domain.Entities;
using AbyDemo.Cart.Domain.Events;

namespace AbyDemo.Cart.Application.ShoppingCartUseCases;

public class ClearCart(
    ICartRepository repository,
    ICacheService cache,
    IGetCart getCart,
    IEventPublisher eventPublisher
) : IClearCart
{
    private readonly ICartRepository _repository = repository;
    private readonly ICacheService _cache = cache;
    private readonly IGetCart _getCart = getCart;
    private readonly IEventPublisher _eventPublisher = eventPublisher;

    public async Task<ShoppingCart> Execute(string userId)
    {
        var cart = await _getCart.Execute(userId);
        await _cache.Delete(userId);
        await _repository.DeleteCart(userId);

        await _eventPublisher.PublishAsync(
            userId,
            new CartClearedEvent
            {
                UserId = userId,
                Products = cart.CartItems.Select(i => i.ProductId).ToList(),
            }
        );
        return await _getCart.Execute(userId);
    }
}
