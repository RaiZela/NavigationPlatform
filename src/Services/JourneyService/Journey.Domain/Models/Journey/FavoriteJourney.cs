using Journey.Domain.Models.Auth;
using System.ComponentModel.DataAnnotations.Schema;

namespace Journey.Domain.Models.Journey;

public class FavoriteJourney : Aggregate<Guid>
{
    private FavoriteJourney()
    { }

    [ForeignKey(nameof(Journey.Id))]
    public Guid JourneyId { get; set; }
    public Journey Journey { get; set; }

    [ForeignKey(nameof(User.Id))]
    public Guid UserId { get; set; }
    public User User { get; set; }


    public static FavoriteJourney Create(Guid userId, Guid journeyId)
    {
        var favorite = new FavoriteJourney
        {
            UserId = userId,
            JourneyId = journeyId
        };

        favorite.AddDomainEvent(new JourneyFavoritedEvent(favorite));
        return favorite;
    }

    public void Remove()
    {
        AddDomainEvent(new JourneyUnfavoritedEvent(Id));
    }
}
