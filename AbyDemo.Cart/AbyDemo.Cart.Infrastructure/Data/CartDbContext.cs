using AbyDemo.Cart.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace AbyDemo.Cart.Infrastructure.Data;

public static class CartDbContext
{
    public static void Configure()
    {
        BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(GuidRepresentation.Standard));
        BsonClassMap.RegisterClassMap<ShoppingCart>(classMap =>
        {
            classMap.AutoMap();
            classMap.MapIdMember(c => c.UserId);
        });
    }
}
