namespace Journey.Application.Journeys.EventHandlers;

public class JourneyUpdatedEventHandler(ILogger<JourneyUpdatedEventHandler> logger)
    : INotificationHandler<JourneyUpdatedEvent>
{
    public Task Handle(JourneyUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event Handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
