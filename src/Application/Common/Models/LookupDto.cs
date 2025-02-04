using AutoMapper;
using Void.Chef.Domain.Entities;

namespace Void.Chef.Application.Common.Models;

public class LookupDto
{
    public int Id { get; init; }

    public string? Title { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, LookupDto>();
        }
    }
}
