using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Void.Chef.Application.Common.Interfaces;
using Void.Chef.Application.Common.Mappings;
using Void.Chef.Application.Common.Models;

namespace Void.Chef.Application.Products.Queries.GetProductsWithPagination;

public record GetProductsWithPaginationQuery : IRequest<PaginatedList<ProductBriefDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, PaginatedList<ProductBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProductBriefDto>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .OrderBy(x => x.Name)
            .ProjectTo<ProductBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
