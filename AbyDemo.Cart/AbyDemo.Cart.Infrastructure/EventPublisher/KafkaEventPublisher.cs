using AbyDemo.Cart.Domain.Contracts.Events;

public class KafkaEventPublisher : IEventPublisher
{
    public KafkaEventPublisher() { }

    public Task PublishAsync<T>(
        string key,
        T @event,
        string topic = "cart-events",
        CancellationToken cancellationToken = default
    )
    {
        return Task.CompletedTask;
    }
}
