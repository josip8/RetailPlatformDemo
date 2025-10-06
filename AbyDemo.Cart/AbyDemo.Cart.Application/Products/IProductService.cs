using AbyDemo.Cart.Application.Products.Models;

namespace AbyDemo.Cart.Application.Products;

public interface IProductService
{
    Task<ProductInfo?> GetProductById(Guid productId);
}
