namespace Journey.Application.Journeys.EventHandlers.Journeys;

public class JourneyUpdatedEventHandler(
    ILogger<JourneyUpdatedEventHandler> logger,
      IPublishEndpoint publishEndpoint)
    : INotificationHandler<JourneyUpdatedEvent>
{
    public async Task Handle(JourneyUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event Handled: {DomainEvent}", notification.GetType().Name);

        await publishEndpoint.Publish(notification, cancellationToken);
    }
}
