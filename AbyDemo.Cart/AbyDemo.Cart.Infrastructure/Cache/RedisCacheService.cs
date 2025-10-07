using AbyDemo.Cart.Domain.Contracts.Cache;
using AbyDemo.Cart.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AbyDemo.Cart.Infrastructure.Cache;

public class RedisCacheService(IDistributedCache redis) : ICacheService
{
    private readonly IDistributedCache _redis = redis;
    private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task<ShoppingCart?> Get(string key)
    {
        var value = await _redis.GetAsync(key);

        if (value == null)
        {
            return null;
        }

        return JsonSerializer.Deserialize<ShoppingCart>(value, _serializerOptions);
    }

    public async Task Set(string key, ShoppingCart cart)
    {
        var serialized = JsonSerializer.SerializeToUtf8Bytes(cart, _serializerOptions);
        await _redis.SetAsync(key, serialized, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60) });
    }

    public async Task Delete(string key)
    {
        await _redis.RemoveAsync(key);
    }
}
