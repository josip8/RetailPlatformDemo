namespace AbyDemo.Cart.Application.ShoppingCartUseCases.Models;

public record UpsertCartItemDto(Guid ProductId, int Quantity);
