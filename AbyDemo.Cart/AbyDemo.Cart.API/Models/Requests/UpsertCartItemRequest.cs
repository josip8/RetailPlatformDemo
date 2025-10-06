namespace AbyDemo.Cart.API.Models.Requests;

public class UpsertCartItemRequest
{
    public required Guid ProductId { get; set; }
    public required int Quantity { get; set; }
}
