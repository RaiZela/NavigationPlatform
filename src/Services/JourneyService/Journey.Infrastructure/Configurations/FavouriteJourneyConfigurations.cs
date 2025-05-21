using Journey.Domain.Models.Journey;

namespace Journey.Infrastructure.Configurations;

public class FavouriteJourneyConfigurations : IEntityTypeConfiguration<FavoriteJourney>
{
    public void Configure(EntityTypeBuilder<FavoriteJourney> builder)
    {
        builder
      .HasKey(fj => new { fj.JourneyId, fj.UserId });

        builder
            .HasOne(fj => fj.Journey)
            .WithMany()
        .HasForeignKey(fj => fj.JourneyId)
        .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(fj => fj.User)
            .WithMany()
            .HasForeignKey(fj => fj.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CreatedByUser)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
