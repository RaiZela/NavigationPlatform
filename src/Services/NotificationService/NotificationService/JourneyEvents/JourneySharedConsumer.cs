using BuildingBlocks.IntegrationEvents.Events.SharedJourneyEvents;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;

namespace NotificationService.JourneyEvents;

public class JourneySharedConsumer : IConsumer<JourneySharedIntegrationEvent>
{
    private readonly ILogger<JourneySharedConsumer> _logger;
    private readonly IHubContext<NotificationHub> _hub;

    public JourneySharedConsumer(ILogger<JourneySharedConsumer> logger, IHubContext<NotificationHub> hub)
    {
        _logger = logger;
        _hub = hub;
    }

    public async Task Consume(ConsumeContext<JourneySharedIntegrationEvent> context)
    {
        var e = context.Message;
        _logger.LogInformation("Journey Shared: {DistanceKm}", e.Journey.DistanceKm);

        await _hub.Clients.All.SendAsync("JourneyShared", new
        {
            e.Journey.DistanceKm,
            e.Journey.CreatedByUser,
            Message = $"Journey '{e.Journey.ArrivalLocation}' was shared!"
        });
    }
}
