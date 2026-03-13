namespace Catalog.API.Products.DeleteProduct;

//public record DeleteProductCommand(Guid Id) 
public record DeleteProductRequest(bool IsSuccess);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id:guid}", 
            async (Guid id, ISender sender, CancellationToken cancellation = default) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id), cancellation);

                var response = result.Adapt<DeleteProductRequest>();

                return Results.Ok(response);
            })
            .WithName("DeleteProduct")
            .Produces<DeleteProductRequest>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Deletes a product by ID")
            .WithDescription("Deletes a product by its unique identifier and returns the result of the operation.");
    }
}
