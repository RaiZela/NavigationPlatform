using Journey.Application.Journeys.Commands.FavoriteJourneys.AddFavoriteJourney;

namespace Journey.API.Endpoints;

public record FavoriteJourneyRequest(Guid JourneyId);

public record FavoriteJourneyResponse(bool IsSuccess);

public class FavoriteJourney : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/journeys/favorite", async (FavoriteJourneyRequest request, ISender sender) =>
        {
            if (request.JourneyId == Guid.Empty)
            {
                return Results.BadRequest("JourneyId is empty");
            }
            var command = request.Adapt<FavoritedJourneyCommand>();
            var result = await sender.Send(command);
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
