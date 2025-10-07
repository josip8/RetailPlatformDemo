using AbyDemo.Cart.API.Models.Requests;
using AbyDemo.Cart.API.Models.Responses;
using AbyDemo.Cart.Application.ShoppingCartUseCases.Contracts;
using AbyDemo.Cart.Application.ShoppingCartUseCases.Models;
using Microsoft.AspNetCore.Mvc;

namespace AbyDemo.Cart.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{
    private readonly IGetCart _getCart;
    private readonly IClearCart _clearCart;
    private readonly IUpsertCartItem _upsertCartItem;
    private readonly IRemoveCartItem _removeCartItem;
    private readonly ILogger<CartController> _logger;

    public CartController(
        IGetCart getCart,
        IClearCart clearCart,
        IUpsertCartItem upsertCartItem,
        IRemoveCartItem removeCartItem,
        ILogger<CartController> logger
    )
    {
        _getCart = getCart;
        _clearCart = clearCart;
        _upsertCartItem = upsertCartItem;
        _removeCartItem = removeCartItem;
        _logger = logger;
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(CartResponse), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<ActionResult<CartResponse>> GetCart(string userId)
    {
        var cart = await _getCart.Execute(userId);
        return Ok(new CartResponse(cart));
    }

    [HttpPost("{userId}/items")]
    [ProducesResponseType(typeof(CartResponse), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<ActionResult<CartResponse>> UpsertItem(
        string userId,
        [FromBody] UpsertCartItemRequest request
    )
    {
        var dto = new UpsertCartItemDto(request.ProductId, request.Quantity);
        var cart = await _upsertCartItem.Execute(userId, dto);
        return Ok(new CartResponse(cart));
    }

    [HttpDelete("{userId}/items/{productId}")]
    [ProducesResponseType(typeof(CartResponse), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<ActionResult<CartResponse>> RemoveItem(string userId, Guid productId)
    {
        var cart = await _removeCartItem.Execute(userId, productId);
        return Ok(new CartResponse(cart));
    }

    [HttpDelete("{userId}")]
    [ProducesResponseType(typeof(CartResponse), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<ActionResult<CartResponse>> ClearCart(string userId)
    {
        var cart = await _clearCart.Execute(userId);
        return Ok(new CartResponse(cart));
    }
}
