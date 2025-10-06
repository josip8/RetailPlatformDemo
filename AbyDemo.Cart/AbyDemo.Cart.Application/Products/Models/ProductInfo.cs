namespace AbyDemo.Cart.Application.Products.Models;

public class ProductInfo
{
    public Guid ProductId { get; set; }
    public required string ProductName { get; set; }
    public decimal Price { get; set; }
}
