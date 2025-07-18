using Journey.Application.Journeys.Commands.FavoriteJourneys.AddFavoriteJourney;

namespace Journey.API.Endpoints;

public record FavoriteJourneyRequest(Guid JourneyId);

public record FavoriteJourneyResponse(bool IsSuccess);

public class FavoriteJourney : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/journeys/favorite/{id}", async (Guid id, ISender sender) =>
        {
            if (id == Guid.Empty)
            {
                return Results.BadRequest("JourneyId is empty");
            }
            var result = await sender.Send(new FavoritedJourneyCommand(id));
            var response = result.Adapt<FavoriteJourneyResponse>();
            return Results.Ok(response);
        })
        .WithName("FavoriteJourney")
        .Produces<FavoriteJourneyResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Favorite Journey")
        .WithDescription("Favorite Journey")
        .RequireAuthorization("authenticated");
    }
}
