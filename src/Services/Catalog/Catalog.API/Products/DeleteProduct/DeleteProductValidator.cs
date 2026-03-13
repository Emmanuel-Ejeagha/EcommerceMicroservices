namespace Catalog.API.Products.DeleteProduct;

public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Product ID is required.")
            .Must(id => id != Guid.Empty).WithMessage("Product ID must be a valid GUID.");
    }
}
