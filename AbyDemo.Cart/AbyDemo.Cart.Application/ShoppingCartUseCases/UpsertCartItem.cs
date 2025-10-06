using AbyDemo.Cart.Application.Products;
using AbyDemo.Cart.Application.Products.Exceptions;
using AbyDemo.Cart.Application.ShoppingCartUseCases.Contracts;
using AbyDemo.Cart.Application.ShoppingCartUseCases.Models;
using AbyDemo.Cart.Domain.Contracts.Cache;
using AbyDemo.Cart.Domain.Contracts.Repositories;
using AbyDemo.Cart.Domain.Entities;

namespace AbyDemo.Cart.Application.ShoppingCartUseCases;

public class UpsertCartItem(
    ICartRepository repository,
    ICacheService cache,
    IGetCart getCart,
    IRemoveCartItem removeCartItem,
    IProductService productService
) : IUpsertCartItem
{
    private readonly ICartRepository _repository = repository;
    private readonly ICacheService _cache = cache;
    private readonly IGetCart _getCart = getCart;
    private readonly IRemoveCartItem _removeCartItem = removeCartItem;
    private readonly IProductService _productService = productService;

    public async Task<ShoppingCart> Execute(string userId, UpsertCartItemDto upsertCartItemDto)
    {
        if (upsertCartItemDto.Quantity <= 0)
        {
            return await _removeCartItem.Execute(userId, upsertCartItemDto.ProductId);
        }

        var cart = await _getCart.Execute(userId);
        var existingItem = cart.CartItems.FirstOrDefault(i =>
            i.ProductId == upsertCartItemDto.ProductId
        );

        if (existingItem != null)
        {
            existingItem.Quantity = upsertCartItemDto.Quantity;
        }
        else
        {
            var productInfo = await _productService.GetProductById(upsertCartItemDto.ProductId);
            if (productInfo == null)
            {
                throw new ProductNotFoundException(upsertCartItemDto.ProductId.ToString());
            }

            cart.CartItems.Add(
                new ShoppingCartItem
                {
                    ProductId = productInfo.ProductId,
                    ProductName = productInfo.ProductName,
                    Price = productInfo.Price,
                    Quantity = upsertCartItemDto.Quantity,
                }
            );
        }

        await _cache.Set(userId, cart);
        var savedCart = await _repository.SaveCart(cart);

        return savedCart;
    }
}
