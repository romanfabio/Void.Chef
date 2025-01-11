using System.Text.Json.Serialization;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace Void.Chef.Features.Shared.Models;

public class ChatMessage(string role, string? content)
{
    [JsonPropertyName("role")] public string Role { get; init; } = role;

    [JsonPropertyName("content")] public string? Content { get; init; } = content;

    public static ChatMessage FromChatMessageContent(ChatMessageContent message)
    {
        if (message.Role.Label == AuthorRole.User.Label)
            return new ChatMessage("user", message.Content);
        if (message.Role.Label == AuthorRole.Assistant.Label)
            return new ChatMessage("assistant", message.Content);
        if(message.Role.Label == AuthorRole.System.Label)
            return new ChatMessage("system", message.Content);
        return new ChatMessage("", message.Content);
    }
}
