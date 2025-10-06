using AbyDemo.Cart.Application.ShoppingCartUseCases.Models;
using AbyDemo.Cart.Domain.Entities;

namespace AbyDemo.Cart.Application.ShoppingCartUseCases.Contracts;

public interface IUpsertCartItem
{
    Task<ShoppingCart> Execute(string userId, UpsertCartItemDto upsertCartItemDto);
}
