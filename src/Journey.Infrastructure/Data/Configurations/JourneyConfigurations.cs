namespace Journey.Infrastructure.Data.Configurations;

public class JourneyConfigurations : IEntityTypeConfiguration<JourneyEntity>
{
    public void Configure(EntityTypeBuilder<JourneyEntity> builder)
    {
        builder.HasKey(x => x.Id);


        builder.HasIndex(x => x.StartLocation).IsUnique(false);

        builder.HasIndex(x => x.ArrivalLocation).IsUnique(false);

        builder.Property(j => j.DistanceKm)
                 .HasConversion(
                     v => v.value,
                     v => new DistanceKM(v))
                 .HasPrecision(5, 2)
                 .HasColumnName("DistanceKm");

    }
}
