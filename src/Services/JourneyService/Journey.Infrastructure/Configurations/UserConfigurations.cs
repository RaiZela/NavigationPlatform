using Journey.Domain.Models.Auth;

namespace Journey.Infrastructure.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(u => u.Username).IsUnique();

        builder.HasMany(x => x.FavouriteJourneys)
            .WithOne(x => x.ActionUser)
            .HasForeignKey(x => x.ActionUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}
