using System.Text.Json.Serialization;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace Void.Chef.Features.Shared.Models;

public class ChatRequest
{
    [JsonPropertyName("model")] public string Model { get; set; } = null!;
    
    [JsonPropertyName("messages")] public IEnumerable<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
    
    [JsonPropertyName("stream")] public bool Stream { get; set; }
    
    [JsonPropertyName("temperature")] public float Temperature { get; set; }


    public static ChatRequest FromChatHistory(ChatHistory chatHistory, PromptExecutionSettings? executionSettings)
    {
        return new ChatRequest()
        {
            Model = executionSettings?.ModelId ?? string.Empty,
            Messages = chatHistory
                .Select(ChatMessage.FromChatMessageContent)
        };
    }
    
}