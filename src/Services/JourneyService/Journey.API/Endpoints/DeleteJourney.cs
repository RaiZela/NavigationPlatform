namespace Journey.API.Endpoints;

public record DeleteJourneyResponse(bool IsSuccess);
public class DeleteJourney : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/journeys/{id}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteJourneyCommand(Id));

            var response = new DeleteJourneyResponse(result.result);

            return Results.Ok(response);
        })
            .WithName("DeleteJourney")
.Produces<DeleteJourneyResponse>(StatusCodes.Status201Created)
.ProducesProblem(StatusCodes.Status400BadRequest)
.WithSummary("Delete Journey")
.WithDescription("Delete Journey");
    }
}
