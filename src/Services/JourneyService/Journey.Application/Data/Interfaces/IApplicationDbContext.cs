using Journey.Domain.Models.Auth;
using Journey.Domain.Models.Journey;
using Journey.Domain.Outbox;

namespace Journey.Application.Data.Interfaces;

public interface IApplicationDbContext
{
    DbSet<JourneyEntity> Journeys { get; }
    DbSet<User> Users { get; }
    DbSet<SharedJourney> SharedJourneys { get; set; }
    DbSet<FavoriteJourney> FavoriteJourneys { get; set; }
    DbSet<OutboxMessage> OutboxMessages { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
