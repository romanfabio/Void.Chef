using System.Reflection;
using Void.Chef.Application.Common.Interfaces;
using Void.Chef.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Void.Chef.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    
    public DbSet<Chat> Chats => Set<Chat>();
    
    public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
