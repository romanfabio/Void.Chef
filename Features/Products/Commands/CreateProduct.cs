using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Void.Chef.Data;
using Void.Chef.Data.Entities;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Void.Chef.Features.Products.Commands;

public record CreateProductCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
    
    public double Quantity { get; set; }
    
    public int CategoryId { get; set; }
    
    public int UnitOfMeasureId { get; set; }
}

public class CreateProductHandler(ApplicationDbContext context) : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var unit = await context.UnitOfMeasures.SingleOrDefaultAsync(u => u.Id == command.UnitOfMeasureId, cancellationToken);
        if(unit == null)
            throw new ValidationException($"Unit of measure with id {command.UnitOfMeasureId} was not found");
        
        var category = await context.Categories.SingleOrDefaultAsync(c => c.Id == command.CategoryId, cancellationToken);
        if (category == null)
            throw new ValidationException($"Category with id {command.CategoryId} was not found");

        var product = new Product()
        {
            Name = command.Name,
            Quantity = command.Quantity,
            Category = category,
            UnitOfMeasure = unit,
        };
        
        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);
        return product.Id;
    }
}

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.CategoryId).GreaterThan(0);
        RuleFor(x => x.UnitOfMeasureId).GreaterThan(0);
    }
}