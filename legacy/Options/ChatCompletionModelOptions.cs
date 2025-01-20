namespace Void.Chef.Options;

public class ChatCompletionModelOptions
{
    public const string Position = "ChatCompletionModel";
    
    public required string Endpoint { get; set; }
    
    public required string Name { get; set; }
    
    public required string ApiKey { get; set; }
}