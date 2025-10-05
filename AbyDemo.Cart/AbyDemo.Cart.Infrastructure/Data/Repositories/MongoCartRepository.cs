using AbyDemo.Cart.Domain.Contracts.Repositories;
using AbyDemo.Cart.Domain.Entities;
using MongoDB.Driver;

namespace AbyDemo.Cart.Infrastructure.Data.Repositories;

public class MongoCartRepository: ICartRepository
{
    private readonly IMongoCollection<ShoppingCart> _collection;

    public MongoCartRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<ShoppingCart>("carts");
    }

    public async Task<ShoppingCart> GetCartAsync(string userId)
    {
        var filter = Builders<ShoppingCart>.Filter.Eq(c => c.UserId, userId);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<ShoppingCart> SaveCartAsync(ShoppingCart cart)
    {
        cart.UpdateAt = DateTime.UtcNow;

        var filter = Builders<ShoppingCart>.Filter.Eq(c => c.UserId, cart.UserId);
        var options = new ReplaceOptions { IsUpsert = true };

        await _collection.ReplaceOneAsync(filter, cart, options);
        return cart;

    }

    public async Task<bool> DeleteCartAsync(string userId)
    {
        var filter = Builders<ShoppingCart>.Filter.Eq(c => c.UserId, userId);
        var result = await _collection.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
}
