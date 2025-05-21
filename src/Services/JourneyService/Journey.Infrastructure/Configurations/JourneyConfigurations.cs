using Journey.Domain.Models.Journey;

namespace Journey.Infrastructure.Configurations;

public class JourneyConfigurations : IEntityTypeConfiguration<JourneyEntity>
{
    public void Configure(EntityTypeBuilder<JourneyEntity> builder)
    {
        builder.HasKey(x => x.Id);


        builder.HasIndex(x => x.StartLocation).IsUnique(false);

        builder.HasIndex(x => x.ArrivalLocation).IsUnique(false);

        builder.Property(j => j.DistanceKm)
                 .HasConversion(
                     v => v.Value,
                     v => new DistanceKM(v))
                 .HasPrecision(5, 2)
                 .HasColumnName("DistanceKm");

        builder.HasOne(j => j.CreatedByUser)
   .WithMany(u => u.CreatedJourneys)
   .HasForeignKey(j => j.CreatedByUserId)
   .OnDelete(DeleteBehavior.Restrict);
    }


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
