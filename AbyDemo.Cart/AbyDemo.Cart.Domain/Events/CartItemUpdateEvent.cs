namespace AbyDemo.Cart.Domain.Events;

public class CartItemUpdateEvent : CartEvent
{
    public CartItemUpdateEvent()
        : base("CartItemUpdate") { }

    public required Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int OldQuantity { get; set; }
}
