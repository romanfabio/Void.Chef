using AutoMapper;
using Void.Chef.Domain.Entities;

namespace Void.Chef.Application.Products.Queries.GetProductsWithPagination;
public class ProductBriefDto
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductBriefDto>();
        }
    }
}
