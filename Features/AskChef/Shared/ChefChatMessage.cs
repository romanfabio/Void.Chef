namespace Void.Chef.Features.AskChef.Shared;

public class ChefChatMessage
{
    public string Content { get; set; } = null!;
    
    public bool SentByUser { get; set; }
}