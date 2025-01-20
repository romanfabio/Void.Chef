using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Void.Chef.Data;
using Void.Chef.Data.Entities;
using Void.Chef.Features.Shared.Dtos;

namespace Void.Chef.Features.Categories.Queries;

public record GetCategoriesQuery() : IRequest<IEnumerable<SimpleLookup>>;

public class GetCategoriesHandler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<GetCategoriesQuery, IEnumerable<SimpleLookup>>
{
    public async Task<IEnumerable<SimpleLookup>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        return await mapper.ProjectTo<SimpleLookup>(
                context.Categories.AsNoTracking())
            .ToArrayAsync(cancellationToken);
    }
}

public class GetCategoriesProfile : Profile
{
    public GetCategoriesProfile()
    {   
        CreateProjection<Category, SimpleLookup>();
    }
}