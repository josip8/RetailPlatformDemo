using AbyDemo.Cart.Domain.Entities;

namespace AbyDemo.Cart.Application.ShoppingCartUseCases.Contracts;

public interface IClearCart
{
    Task<ShoppingCart> Execute(string userId);
}
