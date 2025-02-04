using FluentValidation;

namespace Void.Chef.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(200)
            .NotEmpty();
    }
}
