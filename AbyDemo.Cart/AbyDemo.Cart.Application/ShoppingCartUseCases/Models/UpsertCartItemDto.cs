namespace AbyDemo.Cart.Application.ShoppingCartUseCases.Models;

public class UpsertCartItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
