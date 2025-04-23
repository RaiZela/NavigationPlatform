namespace Journey.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<JourneyEntity> Journeys { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<SharedJourney> SharedJourneys { get; set; }
    public DbSet<FavoriteJourney> FavoriteJourneys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
