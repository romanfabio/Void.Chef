using System.Collections;
using System.Text.Json.Serialization;

namespace Void.Chef.Features.Shared.Models;

public class ChatResponse
{
    [JsonPropertyName("choices")] public IEnumerable<ChatResponseChoice> Choices { get; set; } = new List<ChatResponseChoice>();
}

public class ChatResponseChoice
{
    [JsonPropertyName("index")] public int Index { get; set; }

    [JsonPropertyName("message")] public ChatMessage Message { get; set; } = null!;
}