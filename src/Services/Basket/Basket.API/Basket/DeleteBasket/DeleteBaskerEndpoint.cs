namespace Basket.API.Basket.DeleteBasket;

//public record DeleteBasketRequest(string UserName);
public record DeleteBasketResponse(bool IsSuccess);
public class DeleteBaskerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}",
            async (string userName, ISender sender, CancellationToken cancellationToken = default) =>
        {
            var result = await sender.Send(new DeleteBasketCommand(userName), cancellationToken);

            var response = result.Adapt<DeleteBasketResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteBasket")
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Deletes the shopping basket for a specific user.")
        .WithDescription("Deletes the shopping basket for a specific user identified by their username. " +
        "If the basket is successfully deleted, it returns a response indicating success. " +
        "If the user or basket is not found, it returns an appropriate error response.");
    }
}