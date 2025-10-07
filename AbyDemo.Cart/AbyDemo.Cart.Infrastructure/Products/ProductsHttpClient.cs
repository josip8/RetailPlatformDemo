using AbyDemo.Cart.Application.Products;
using AbyDemo.Cart.Application.Products.Models;

namespace AbyDemo.Cart.Infrastructure.Products;

public class ProductsHttpClient : IProductService
{
    private readonly HttpClient _httpClient;
    public ProductsHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<ProductInfo?> GetProductById(Guid productId)
    {
        return Task.FromResult<ProductInfo?>(new ProductInfo(productId, "Sample Product", 1.99m));
    }
}
