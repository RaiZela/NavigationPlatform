
using Journey.Application.Journeys.Commands.UpdateJourney;

namespace Journey.API.Endpoints
{
    //Accept a UpdateJourneyRequest object
    //Maps the request to a UpdateOrderCommand
    //Uses MediatR to send the command to the corresponding handler
    //Returns a response with the created order's ID

    public record UpdateourneyRequest(JourneyDto Journey);
    public record UpdateJourneyResponse(Guid Id);
    public class UpdateJourney : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/journeys", async (CreateJourneyRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateJourneyCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateJourneyResponse>();

                return Results.Ok(response);

            })
                .WithName("UpdateJourney")
            .Produces<CreateJourneyResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Journey")
            .WithDescription("Update Journey");
        }
    }
}
