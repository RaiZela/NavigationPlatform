using Journey.Application.Journeys.Queries.GetFavouriteJourneys;

namespace Journey.API.Endpoints;
public record GetFavoriteJourneysResponse(IEnumerable<JourneyDto> favoriteJourneys);
public class GetFavoriteJourneys : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/journeys/favorite-journeys/{userId}", async (Guid userId, ISender sender) =>
        {
            var result = await sender.Send(new GetFavouriteJourneysQuery(userId));
            return Results.Ok(result);
        }).WithName("GetFavoriteJourneys")
            .Produces<CreateJourneyResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Favorite Journey")
            .WithDescription("Favorite Journey")
            .RequireAuthorization("authenticated");
    }
}
