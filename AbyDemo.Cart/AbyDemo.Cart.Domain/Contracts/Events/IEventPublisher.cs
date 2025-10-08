namespace AbyDemo.Cart.Domain.Contracts.Events;

public interface IEventPublisher
{
    Task PublishAsync<T>(
        string key,
        T @event,
        string topic = "cart-events",
        CancellationToken cancellationToken = default
    );
}
