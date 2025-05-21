namespace Journey.API.Endpoints;

public record UpdateourneyRequest(JourneyDto Journey);
public record UpdateJourneyResponse(bool isSuccess);
public class UpdateJourney : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/journeys", async (CreateJourneyRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateJourneyCommand>();

            var result = await sender.Send(command);

            var response = new UpdateJourneyResponse(result.isSuccess); ;

            return Results.Ok(response);

        })
        .WithName("UpdateJourney")
        .Produces<CreateJourneyResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Journey")
        .WithDescription("Update Journey")
        .RequireAuthorization("authenticated");
    }
}
