using AutoMapper;
using Void.Chef.Domain.Entities;

namespace Void.Chef.Application.Common.Models;

public class LookupDto
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, LookupDto>();
        }
    }
}