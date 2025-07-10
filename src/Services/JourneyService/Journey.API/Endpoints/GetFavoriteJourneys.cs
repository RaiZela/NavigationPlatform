
using Journey.Application.Journeys.Queries.GetFavouriteJourneys;

namespace Journey.API.Endpoints;

public record GetFavouriteJourneys(IEnumerable<JourneyDto> Journeys);
public class GetFavoriteJourneys : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/journeys/favorite-journeys", async (ISender sender) =>
        {
            var result = await sender.Send(new GetFavouriteJourneysQuery());
            return Results.Ok(result);
        });
        throw new NotImplementedException();
    }
}
