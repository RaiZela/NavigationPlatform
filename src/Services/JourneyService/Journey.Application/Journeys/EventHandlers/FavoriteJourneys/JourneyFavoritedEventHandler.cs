namespace Journey.Application.Journeys.EventHandlers.FavoriteJourneys;

public class JourneyFavoritedEventHandler(
    ILogger<JourneyFavoritedEventHandler> logger,
    IPublishEndpoint publishEndpoint)
    : INotificationHandler<JourneyFavoritedEvent>
{
    public async Task Handle(JourneyFavoritedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event Handled: {DomainEvent}", notification.GetType().Name);
        try
        {
            var model = new FavoriteJourneyEventModel
            {
                Username = notification.journey.User.Username,
                ArrivalLocation = notification.journey.Journey.ArrivalLocation,
                CreatedByUser = notification.journey.Journey.CreatedByUser.Username
            };

            var message = new FavoritedJourneyIntegrationEvent
            {
                Journey = model
            };

            await publishEndpoint.Publish(message, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogInformation("JourneyFavoritedEvent EXCEPTION" + ex.Message);
            throw;
        }
    }
}

