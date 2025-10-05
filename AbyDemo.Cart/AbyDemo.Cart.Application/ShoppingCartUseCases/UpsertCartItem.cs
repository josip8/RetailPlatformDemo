using AbyDemo.Cart.Application.Contracts;
using AbyDemo.Cart.Application.ProductService;
using AbyDemo.Cart.Application.ShoppingCartUseCases.Contracts;
using AbyDemo.Cart.Application.ShoppingCartUseCases.Models;
using AbyDemo.Cart.Domain.Contracts.Repositories;
using AbyDemo.Cart.Domain.Entities;

namespace AbyDemo.Cart.Application.ShoppingCartUseCases;

public class UpsertCartItem
{
    private readonly ICartRepository _repository;
    private readonly ICacheService _cache;
    private readonly IGetCart _getCart;
    private readonly IProductService _productService;

    public UpsertCartItem(
        ICartRepository repository,
        ICacheService cache,
        IGetCart getCart,
        IProductService productService
    )
    {
        _repository = repository;
        _cache = cache;
        _getCart = getCart;
        _productService = productService;
    }
}
