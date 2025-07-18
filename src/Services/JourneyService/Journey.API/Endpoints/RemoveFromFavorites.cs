using Journey.Application.Journeys.Commands.FavoriteJourneys.UnfavoriteJourney;

namespace Journey.API.Endpoints;

public record UnfavoriteJourneyResponse(bool isSuccess);
public class RemoveFromFavorites : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/journeys/{id}", async (Guid id, ISender sender) =>
        {
            var command = new UnfavouriteJourneyCommand(id);
            var result = await sender.Send(command);
            var response = new UnfavoriteJourneyResponse(result.IsSuccess);
            return Results.Ok(response);
        })
        .WithName("RemoveFromFavorites")
        .Produces<UnfavoriteJourneyResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Remove Journey from Favorites")
        .WithDescription("Remove Journey from Favorites")
        .RequireAuthorization("authenticated");
    }
}
