namespace AbyDemo.Cart.Application.Products.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(string productId)
        : base($"Product with ID '{productId}' was not found.") { }
}
