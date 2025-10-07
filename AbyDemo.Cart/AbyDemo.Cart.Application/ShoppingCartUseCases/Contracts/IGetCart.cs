using AbyDemo.Cart.Domain.Entities;

namespace AbyDemo.Cart.Application.ShoppingCartUseCases.Contracts;

public interface IGetCart
{
    Task<ShoppingCart> Execute(string userId);
}
