using AbyDemo.Cart.Domain.Entities;

namespace AbyDemo.Cart.Domain.Contracts.Repositories;

public interface ICartRepository
{
    Task<ShoppingCart> GetCart(string userId);
    Task<ShoppingCart> SaveCart(ShoppingCart cart);
    Task<bool> DeleteCart(string userId);
}
