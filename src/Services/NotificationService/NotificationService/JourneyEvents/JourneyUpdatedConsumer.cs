using BuildingBlocks.IntegrationEvents.Events.JourneyEvents;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;

namespace NotificationService.JourneyEvents;

public class JourneyUpdatedConsumer : IConsumer<JourneyUpdatedIntegrationEvent>
{
    private readonly ILogger<JourneyUpdatedConsumer> _logger;
    private readonly IHubContext<NotificationHub> _hub;

    public JourneyUpdatedConsumer(ILogger<JourneyUpdatedConsumer> logger, IHubContext<NotificationHub> hub)
    {
        _logger = logger;
        _hub = hub;
    }

    public async Task Consume(ConsumeContext<JourneyUpdatedIntegrationEvent> context)
    {
        var e = context.Message;
        _logger.LogInformation("Journey shared: {DistanceKm}", e.Journey.DistanceKm);

        await _hub.Clients.All.SendAsync("JourneyCreated", new
        {
            e.Journey.DistanceKm,
            e.Journey.CreatedByUser,
            Message = $"Journey '{e.Journey.ArrivalLocation}' was shared!"
        });
    }
}
