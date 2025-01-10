using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Void.Chef.Data;
using Void.Chef.Data.Entities;
using Void.Chef.Features.Shared.Dtos;

namespace Void.Chef.Features.UnitOfMeasures.Queries;

public record GetUnitOfMeasuresQuery() : IRequest<IEnumerable<SimpleLookup>>;

public class GetUnitOfMeasuresHandler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<GetUnitOfMeasuresQuery, IEnumerable<SimpleLookup>>
{
    public async Task<IEnumerable<SimpleLookup>> Handle(GetUnitOfMeasuresQuery query, CancellationToken cancellationToken)
    {
        return await mapper.ProjectTo<SimpleLookup>(
                context.UnitOfMeasures.AsNoTracking())
            .ToArrayAsync(cancellationToken);
    }
}

public class GetUnitOfMeasuresProfile : Profile
{
    public GetUnitOfMeasuresProfile()
    {   
        CreateProjection<UnitOfMeasure, SimpleLookup>();
    }
}