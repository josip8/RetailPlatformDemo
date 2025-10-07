using AbyDemo.Cart.Domain.Entities;

namespace AbyDemo.Cart.Domain.Contracts.Cache;

public interface ICacheService
{
    Task<ShoppingCart?> Get(string key);
    Task Set(string key, ShoppingCart cart);
    Task Delete(string key);
}
