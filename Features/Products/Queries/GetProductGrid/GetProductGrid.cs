using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Void.Chef.Data;
using Void.Chef.Data.Entities;
using Void.Chef.Features.Shared.Dtos;

namespace Void.Chef.Features.Products.Queries.GetProductGrid;

public record GetProductGridQuery() : IRequest<IEnumerable<GetProductGridResponse>>;

public class GetProductGridResponse
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public double Quantity { get; set; }
    
    public SimpleLookup UnitOfMeasure { get; set; } = null!;

    public SimpleLookup Category { get; set; } = null!;
}

public class GetProductGridHandler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<GetProductGridQuery, IEnumerable<GetProductGridResponse>>
{
    public async Task<IEnumerable<GetProductGridResponse>> Handle(GetProductGridQuery request, CancellationToken cancellationToken)
    {
        return await mapper.ProjectTo<GetProductGridResponse>(
            context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.UnitOfMeasure))
            .ToListAsync(cancellationToken);
    }
}

public class GetProductGridProfile : Profile
{
    public GetProductGridProfile()
    {
        CreateProjection<Product, GetProductGridResponse>();
    }
}