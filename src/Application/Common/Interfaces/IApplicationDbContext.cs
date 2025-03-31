using Microsoft.EntityFrameworkCore;
using Void.Chef.Domain.Entities;

namespace Void.Chef.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    
    DbSet<Chat> Chats { get; }
    
    DbSet<ChatMessage> ChatMessages { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
