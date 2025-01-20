using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Void.Chef.Data;
using Void.Chef.Data.Entities;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Void.Chef.Features.Products.Commands;

public record UpdateProductCommand : IRequest
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public double Quantity { get; set; }
    
    public int CategoryId { get; set; }
    
    public int UnitOfMeasureId { get; set; }
}

public class UpdateProductHandler(ApplicationDbContext context) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var unit = await context.UnitOfMeasures.SingleOrDefaultAsync(u => u.Id == command.UnitOfMeasureId, cancellationToken);
        if(unit == null)
            throw new ValidationException($"Unit of measure with id {command.UnitOfMeasureId} was not found");
        
        var category = await context.Categories.SingleOrDefaultAsync(c => c.Id == command.CategoryId, cancellationToken);
        if (category == null)
            throw new ValidationException($"Category with id {command.CategoryId} was not found");

        var product = await context.Products.SingleOrDefaultAsync(p => p.Id == command.Id, cancellationToken);
        if (product == null)
        {
            throw new ValidationException($"Product with id {command.Id} was not found");
        }
        
        product.Name = command.Name;
        product.Quantity = command.Quantity;
        product.UnitOfMeasure = unit;
        product.Category = category;
        
        await context.SaveChangesAsync(cancellationToken);
    }
}

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.CategoryId).GreaterThan(0);
        RuleFor(x => x.UnitOfMeasureId).GreaterThan(0);
    }
}