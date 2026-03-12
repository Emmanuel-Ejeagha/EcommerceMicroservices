namespace Catalog.API.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file is required.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}
