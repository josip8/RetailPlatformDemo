using MongoDB.Bson.Serialization;
using AbyDemo.Cart.Domain.Entities;

namespace AbyDemo.Cart.Infrastructure.Data;

public class CartDbContext
{
    public void Configure()
    {
        BsonClassMap.RegisterClassMap<ShoppingCart>(classMap =>
        {
            classMap.AutoMap();
            classMap.MapIdMember(c => c.UserId);
        });
    }

}
