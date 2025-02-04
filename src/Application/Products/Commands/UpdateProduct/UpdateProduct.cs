using Ardalis.GuardClauses;
using MediatR;
using Void.Chef.Application.Common.Interfaces;
using Void.Chef.Domain.Events.Products;

namespace Void.Chef.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand : IRequest
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        
        entity.AddDomainEvent(new ProductUpdatedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
