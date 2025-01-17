using System.Text.Json.Serialization;

namespace Void.Chef.Features.Shared.Models;

public class ChatStreamResponseMessageChunk
{
    [JsonPropertyName("choices")]
    public IEnumerable<ChatStreamResponseMessageChunkChoice> Choices { get; set; }
}

public class ChatStreamResponseMessageChunkChoice
{
    [JsonPropertyName("delta")]
    public ChatStreamResponseMessageChunkDelta? Delta { get; set; }
    
    [JsonPropertyName("finish_reason")]
    public string? FinishReason { get; set; }
}

public class ChatStreamResponseMessageChunkDelta
{
    [JsonPropertyName("content")]
    public string Content { get; set; } = null!;
}