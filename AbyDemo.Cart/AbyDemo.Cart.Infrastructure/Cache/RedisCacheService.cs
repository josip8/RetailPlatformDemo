using AbyDemo.Cart.Domain.Entities;
using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AbyDemo.Cart.Infrastructure.Cache;

public class RedisCacheService
{
    private readonly IDatabase _redis;
    private readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(60);
    private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _redis = redis.GetDatabase();
    }

    public async Task<ShoppingCart?> GetAsync(string key)
    {
        var value = await _redis.StringGetAsync(key);

        if (value.IsNullOrEmpty)
        {
            return null;
        }

        return JsonSerializer.Deserialize<ShoppingCart>(value.ToString(), _serializerOptions);
    }

    public async Task SetAsync(string key, ShoppingCart cart, TimeSpan? expiration = null)
    {
        var serialized = JsonSerializer.Serialize(cart, _serializerOptions);
        await _redis.StringSetAsync(key, serialized, expiration ?? _defaultExpiration);
    }

    public async Task DeleteAsync(string key)
    {
        await _redis.KeyDeleteAsync(key);
    }
}
