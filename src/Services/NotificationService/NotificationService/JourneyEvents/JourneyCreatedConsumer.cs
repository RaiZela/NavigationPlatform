using BuildingBlocks.IntegrationEvents.Events.JourneyEvents;
using MassTransit;

namespace NotificationService.JourneyEvents;

public sealed class JourneyCreatedConsumer : IConsumer<JourneyCreatedIntegrationEvent>
{
    private readonly ILogger<JourneyCreatedConsumer> _logger;
    // private readonly IHubContext<NotificationHub> _hub;
    public JourneyCreatedConsumer(
        ILogger<JourneyCreatedConsumer> logger
        //IHubContext<NotificationHub> hub
        )
    {
        _logger = logger;
        //_hub = hub;
    }
    public async Task Consume(ConsumeContext<JourneyCreatedIntegrationEvent> context)
    {
        var e = context.Message;
        _logger.LogInformation("Journey Created: {Journey} by {Username}", e.Journey.ArrivalLocation, e.Journey.LastModifiedByUser);
        //await _hub.Clients.All.SendAsync("JourneyCreated", new
        //{
        //    e.Journey.Id,
        //    e.Journey.LastModifiedByUser,
        //    Message = $"Journey to '{e.Journey.ArrivalLocation}' was created!"
        //});
    }
}
