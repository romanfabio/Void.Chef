using Microsoft.EntityFrameworkCore;
using Void.Chef.Domain.Entities;

namespace Void.Chef.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    
    DbSet<UnitOfMeasure> UnitOfMeasures { get; }
    
    DbSet<Category> Categories { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}