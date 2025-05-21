using Journey.Domain.Models.Journey;

namespace Journey.Infrastructure.Configurations;

public class SharedJourneyConfigurations : IEntityTypeConfiguration<SharedJourney>
{
    public void Configure(EntityTypeBuilder<SharedJourney> builder)
    {
        builder
            .HasKey(sj => new { sj.OwnerId, sj.SharedWIthId, sj.JourneyId });

        builder
            .HasOne(sj => sj.Owner)
            .WithMany()
            .HasForeignKey(sj => sj.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(sj => sj.SharedWIth)
            .WithMany()
            .HasForeignKey(sj => sj.SharedWIthId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(sj => sj.Journey)
            .WithMany()
            .HasForeignKey(sj => sj.JourneyId);
    }
}
