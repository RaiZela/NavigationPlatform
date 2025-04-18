namespace Journey.API.Endpoints;

//Accept a CreateJourneyRequest object
//Maps the request to a CreateOrderCommand
//Uses MediatR to send the command to the corresponding handler
//Returns a response with the created order's ID

public record CreateJourneyRequest(JourneyDto Journey);
public record CreateJourneyResponse(Guid Id);

public class CreateJourney : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/journeys", async (CreateJourneyRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateJourneyCommand>();
            var result = await sender.Send(command);

            var response = result.Adapt<CreateJourneyResponse>();

            return Results.Created($"/journeys/{response.Id}", response);
        })
            .WithName("CreateJourney")
            .Produces<CreateJourneyResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Journey")
            .WithDescription("Create Journey");
    }
}

