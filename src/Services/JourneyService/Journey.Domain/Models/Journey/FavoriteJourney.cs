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

    [ForeignKey(nameof(ActionUser.Id))]
    public Guid ActionUserId { get; set; }
    public User ActionUser { get; set; } //The user that favorited it


    public static FavoriteJourney Create(Guid userId, Guid journeyId)
    {
        var favorite = new FavoriteJourney
        {
            ActionUserId = userId,
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
