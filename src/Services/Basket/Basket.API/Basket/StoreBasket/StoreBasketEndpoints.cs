namespace Basket.API.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);
public record StoreBasketResponse(string UserName);

public class StoreBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket",
            async (StoreBasketRequest request, ISender sender, CancellationToken cancellationToken = default) =>
        {
            var command = request.Adapt<StoreBasketCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<StoreBasketResponse>();

            return Results.Created($"/basket/{response.UserName}", response);
        })
        .WithName("Create ShoppingCart")
        .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Creates a new shopping cart for the specified user.")
        .WithSummary("Create a new shopping cart");
    }
}
