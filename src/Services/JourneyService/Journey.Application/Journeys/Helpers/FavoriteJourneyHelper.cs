using Journey.Domain.Models.Journey;

namespace Journey.Application.Journeys.Helpers;

public static class FavoriteJourneyHelper
{
    public static FavoriteJourney CreateAsFavorite(Guid userId, Guid journeyId)
    {
        var favoriteJourney = FavoriteJourney.Create(userId, journeyId);
        return favoriteJourney;
    }
}
