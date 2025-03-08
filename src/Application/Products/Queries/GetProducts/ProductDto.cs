using AutoMapper;
using Void.Chef.Domain.Entities;

namespace Void.Chef.Application.Products.Queries.GetProducts;

public class ProductDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
