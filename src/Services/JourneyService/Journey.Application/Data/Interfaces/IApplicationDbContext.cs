namespace Journey.Application.Data.Interfaces;

public interface IApplicationDbContext
{
    DbSet<JourneyEntity> Journeys { get; }
    DbSet<User> Users { get; }
    DbSet<SharedJourney> SharedJourneys { get; set; }
    public DbSet<FavoriteJourney> FavoriteJourneys { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
