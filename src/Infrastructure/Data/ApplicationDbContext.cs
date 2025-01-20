using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Void.Chef.Application.Common.Interfaces;
using Void.Chef.Domain.Entities;

namespace Void.Chef.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}