using AbyDemo.Cart.Domain.Contracts.Repositories;
using AbyDemo.Cart.Domain.Entities;
using MongoDB.Driver;

namespace AbyDemo.Cart.Infrastructure.Data.Repositories;

public class MongoCartRepository(IMongoDatabase database) : ICartRepository
{
    private readonly IMongoCollection<ShoppingCart> _collection = database.GetCollection<ShoppingCart>("carts");

    public async Task<ShoppingCart> GetCart(string userId)
    {
        var filter = Builders<ShoppingCart>.Filter.Eq(c => c.UserId, userId);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<ShoppingCart> SaveCart(ShoppingCart cart)
    {
        cart.UpdateAt = DateTime.UtcNow;

        var filter = Builders<ShoppingCart>.Filter.Eq(c => c.UserId, cart.UserId);
        var options = new ReplaceOptions { IsUpsert = true };

        await _collection.ReplaceOneAsync(filter, cart, options);
        return cart;

    }

    public async Task<bool> DeleteCart(string userId)
    {
        var filter = Builders<ShoppingCart>.Filter.Eq(c => c.UserId, userId);
        var result = await _collection.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
}
