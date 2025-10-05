using AbyDemo.Cart.Domain.Entities;

namespace AbyDemo.Cart.Domain.Contracts.Repositories;

public interface ICartRepository
{
    Task<ShoppingCart> GetCartAsync(string userId);
    Task<ShoppingCart> SaveCartAsync(ShoppingCart cart);
    Task<bool> DeleteCartAsync(string userId);
}
