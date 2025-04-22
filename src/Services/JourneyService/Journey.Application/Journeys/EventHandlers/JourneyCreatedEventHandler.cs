using MassTransit;

namespace Journey.Application.Journeys.EventHandlers;
public class JourneyCreatedEventHandler(
    ILogger<JourneyCreatedEventHandler> logger,
    IPublishEndpoint publishEndpoint)
    : INotificationHandler<JourneyCreatedEvent>
{
    public async Task Handle(JourneyCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event Handled: {DomainEvent}", notification.GetType().Name);

        await publishEndpoint.Publish(notification, cancellationToken);

    }
}


