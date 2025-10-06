using AbyDemo.Cart.Domain.Entities;

namespace AbyDemo.Cart.API.Models.Responses;

public class CartResponse
{
    public CartResponse(ShoppingCart cart)
    {
        CartItems =
        [
            .. cart.CartItems.Select(item => new CartItemResponse
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Price = item.Price,
                Quantity = item.Quantity,
                PriceTotal = item.PriceTotal,
            }),
        ];

        CartPrice = cart.CartPrice;
    }

    public List<CartItemResponse> CartItems { get; set; } = new();
    public decimal CartPrice { get; set; }
}
