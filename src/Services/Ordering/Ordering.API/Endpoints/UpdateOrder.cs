using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints;

// Accepts an UpdateOrderRequest
// Maps request to an UpdateOrderCommand
// Sends the command for processing.
// Returns a success or error response on the outcome

public record UpdateOrderRequest(OrderDto Order);

public record UpdateOrderResponse(bool IsSuccess);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<UpdateOrderCommand>();

            var result = await sender.Send(command, cancellationToken);

            var respose = result.Adapt<UpdateOrderResponse>();

            return Results.Ok(respose);
        })
        .WithName("UpdateOrder")
        .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Order")
        .WithDescription("Update Order");
    }
}
