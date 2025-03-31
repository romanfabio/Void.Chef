using Void.Chef.Domain.Common;

namespace Void.Chef.Domain.Entities;

public class ChatMessage : BaseAuditableEntity
{
    public string Content { get; set; } = null!;

    public string AuthorRole { get; set; } = null!;
    
    public DateTime SentAt { get; set; }
    
    public Chat Chat { get; set; } = null!;
}
