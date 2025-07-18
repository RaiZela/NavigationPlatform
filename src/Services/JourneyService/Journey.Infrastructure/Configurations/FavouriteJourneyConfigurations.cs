using Journey.Domain.Models.Journey;

namespace Journey.Infrastructure.Configurations;

public class FavouriteJourneyConfigurations : IEntityTypeConfiguration<FavoriteJourney>
{
    public void Configure(EntityTypeBuilder<FavoriteJourney> builder)
    {
        builder
      .HasKey(fj => new { fj.JourneyId, fj.ActionUserId });

        builder
            .HasOne(fj => fj.Journey)
            .WithMany()
        .HasForeignKey(fj => fj.JourneyId)
        .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(fj => fj.ActionUser)
            .WithMany()
            .HasForeignKey(fj => fj.ActionUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CreatedByUser)
            .WithMany()
            .HasForeignKey(x => x.ActionUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(fj => fj.LastModifiedByUser)
            .WithMany()
            .HasForeignKey(fj => fj.LastModifiedByUserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
