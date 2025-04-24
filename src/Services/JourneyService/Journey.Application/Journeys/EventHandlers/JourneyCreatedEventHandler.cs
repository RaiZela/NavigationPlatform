using Journey.Domain.EventModels;
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

        try
        {
            var message = new JourneyEventModel
            {
                ArrivalLocation = notification.journey.ArrivalLocation,
                ArrivalTime = notification.journey.ArrivalTime,
                StartLocation = notification.journey.StartLocation,
                StartTime = notification.journey.StartTime,
                CreatedAt = notification.journey.CreatedAt,
                CreatedByUser = notification.journey.CreatedByUser.Username,
                LastModified = notification.journey.LastModified,
                LastModifiedByUser = notification.journey.LastModifiedByUser.Username,
                TransportType = (int)notification.journey.TransportType,
                DistanceKm = notification.journey.DistanceKm.Value
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


