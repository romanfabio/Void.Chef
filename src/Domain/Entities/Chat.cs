using Void.Chef.Domain.Common;

namespace Void.Chef.Domain.Entities;

public class Chat : BaseAuditableEntity
{
    public IList<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
}
