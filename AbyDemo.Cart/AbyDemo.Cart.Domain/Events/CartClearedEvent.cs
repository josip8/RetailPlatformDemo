namespace AbyDemo.Cart.Domain.Events;

public class CartClearedEvent : CartEvent
{
    public CartClearedEvent()
        : base("CartCleared") { }
    public List<Guid> Products { get; set; } = new();
}