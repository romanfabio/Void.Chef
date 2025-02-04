using MediatR;
using Void.Chef.Application.Common.Interfaces;
using Void.Chef.Domain.Entities;
using Void.Chef.Domain.Events.Products;

namespace Void.Chef.Application.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<int>
{
    public string Name { get; init; } = null!;
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new Product
        {
            Name = request.Name,
        };

        entity.AddDomainEvent(new ProductCreatedEvent(entity));

        _context.Products.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
