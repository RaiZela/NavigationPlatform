namespace Journey.Domain.Models;

public class FavoriteJourney : Aggregate<Guid>
{
    private FavoriteJourney()
    { }
    public Guid JourneyId { get; set; }
    public Journey Journey { get; set; }
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
        AddDomainEvent(new JourneyUnfavoritedEvent(this.Id));
    }
}
