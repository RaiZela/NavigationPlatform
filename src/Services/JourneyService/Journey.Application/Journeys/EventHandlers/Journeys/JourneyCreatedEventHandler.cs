namespace Journey.Application.Journeys.EventHandlers.Journeys;
public class JourneyCreatedEventHandler(
    ILogger<JourneyCreatedEventHandler> logger,
    IPublishEndpoint publishEndpoint)
    : INotificationHandler<JourneyCreatedEvent>
{
    public async Task Handle(JourneyCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event Handled: {DomainEvent}", notification.GetType().Name);

        try
        {
            var model = new JourneyEventModel
            {
                ArrivalLocation = notification.journey.ArrivalLocation,
                ArrivalTime = notification.journey.ArrivalTime,
                StartLocation = notification.journey.StartLocation,
                StartTime = notification.journey.StartTime,
                CreatedAt = notification.journey.CreatedAt,
                CreatedByUser = notification.journey.CreatedByUser.Username,
                LastModified = notification.journey.LastModified,
                LastModifiedByUser = notification.journey.LastModifiedByUser.Username,
                TransportType = (BuildingBlocks.Enums.TransportType)notification.journey.TransportType,
                DistanceKm = notification.journey.DistanceKm.Value
            };

            var message = new JourneyCreatedIntegrationEvent
            {
                Journey = model
            };
            await publishEndpoint.Publish(message, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogInformation("JourneyCreatedEvent EXCEPTION" + ex.Message);
            throw;
        }

    }
}


