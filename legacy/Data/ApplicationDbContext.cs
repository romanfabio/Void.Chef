using Microsoft.EntityFrameworkCore;
using Void.Chef.Data.Entities;

namespace Void.Chef.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; } = null!;
    
    public DbSet<Category> Categories { get; set; } = null!;
    
    public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; } = null!;
    
    public DbSet<UnitOfMeasureCategory> UnitOfMeasureCategories { get; set; } = null!;
}