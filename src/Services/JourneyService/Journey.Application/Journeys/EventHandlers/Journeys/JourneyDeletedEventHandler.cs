namespace Journey.Application.Journeys.EventHandlers.Journeys;

public class JourneyDeletedEventHandler(
    ILogger<JourneyDeletedEventHandler> logger,
      IPublishEndpoint publishEndpoint)
    : INotificationHandler<JourneyDeletedEvent>
{
    public async Task Handle(JourneyDeletedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event Handled: {DomainEvent}", notification.GetType().Name);

        await publishEndpoint.Publish(notification, cancellationToken);
    }
}
