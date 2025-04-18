namespace Journey.Application.Journeys.EventHandlers;

public class JourneyUpdatedEventHandle(ILogger logger)
    : INotificationHandler<JourneyUpdatedEvent>
{
    public Task Handle(JourneyUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.Information("Domain Event Handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
