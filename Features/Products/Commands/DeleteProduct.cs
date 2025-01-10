using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Void.Chef.Data;

namespace Void.Chef.Features.Products.Commands;

public record DeleteProductCommand(int Id) : IRequest;

public class DeleteProductHandler(ApplicationDbContext context) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await context.Products.SingleOrDefaultAsync(p => p.Id == command.Id, cancellationToken);
        if (product == null)
        {
            throw new ValidationException($"Product with id {command.Id} was not found");
        }
        
        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);
    }
}

public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}