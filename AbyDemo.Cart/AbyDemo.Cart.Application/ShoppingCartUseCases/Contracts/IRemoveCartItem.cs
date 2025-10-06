using AbyDemo.Cart.Domain.Entities;

namespace AbyDemo.Cart.Application.ShoppingCartUseCases.Contracts;

public interface IRemoveCartItem
{
    Task<ShoppingCart> Execute(string userId, Guid productId);
}
