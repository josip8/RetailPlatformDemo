namespace AbyDemo.Cart.Domain.Events;

public abstract class CartEvent
{
    protected CartEvent(string eventType) => EventType = eventType;

    public string EventId { get; set; } = Guid.NewGuid().ToString();
    public string EventType { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public required string UserId { get; set; }
}
