namespace Journey.Application.Journeys.EventHandlers;
public class JourneyCreatedEventHandler(ILogger logger)
    : INotificationHandler<JourneyCreatedEvent>
{
    public Task Handle(JourneyCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.Information("Domain Event Handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}

