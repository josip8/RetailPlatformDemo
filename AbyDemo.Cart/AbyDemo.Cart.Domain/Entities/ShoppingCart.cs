namespace AbyDemo.Cart.Domain.Entities;

public class ShoppingCart
{
    public required string UserId { get; set; }
    public List<ShoppingCartItem> CartItems { get; set; } = new();
    public decimal CartPrice => CartItems.Sum(item => item.PriceTotal);
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
}
