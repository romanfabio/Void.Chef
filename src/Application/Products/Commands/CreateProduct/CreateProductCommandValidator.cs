
using FluentValidation;

namespace Void.Chef.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(200)
            .NotEmpty();
    }
}
