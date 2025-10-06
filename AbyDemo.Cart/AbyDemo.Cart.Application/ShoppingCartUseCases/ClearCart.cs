using AbyDemo.Cart.Application.ShoppingCartUseCases.Contracts;
using AbyDemo.Cart.Domain.Contracts.Cache;
using AbyDemo.Cart.Domain.Contracts.Repositories;
using AbyDemo.Cart.Domain.Entities;

namespace AbyDemo.Cart.Application.ShoppingCartUseCases;

public class ClearCart(ICartRepository repository, ICacheService cache, IGetCart getCart): IClearCart
{
    private readonly ICartRepository _repository = repository;
    private readonly ICacheService _cache = cache;
    private readonly IGetCart _getCart = getCart;

    public async Task<ShoppingCart> Execute(string userId)
    {
        await _cache.Delete(userId);
        await _repository.DeleteCart(userId);
        return await _getCart.Execute(userId);
    }
}
