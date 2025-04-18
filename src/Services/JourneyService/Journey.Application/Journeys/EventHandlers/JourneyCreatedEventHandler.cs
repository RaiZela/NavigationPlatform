namespace Journey.Application.Journeys.EventHandlers;

public class JourneyCreatedEventHandler
    //(ILogger<JourneyCreatedEventHandler> logger)
    : INotificationHandler<JourneyCreatedEvent>
{
    public Task Handle(JourneyCreatedEvent notification, CancellationToken cancellationToken)
    {
        //logger.LogInformation("Domain Event Handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}

