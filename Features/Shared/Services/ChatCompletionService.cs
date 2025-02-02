using System.ClientModel;
using System.Collections.Immutable;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Void.Chef.Features.Shared.Models;
using Void.Chef.Options;
using ChatMessageContent = Microsoft.SemanticKernel.ChatMessageContent;

namespace Void.Chef.Features.Shared.Services;

public class ChatCompletionService : IChatCompletionService
{
    private readonly ChatCompletionModelOptions _options;

    public ChatCompletionService(IOptions<ChatCompletionModelOptions> options)
    {
        _options = options.Value;
    }
    

    public IReadOnlyDictionary<string, object?> Attributes { get; } = new Dictionary<string, object?>();

    public async Task<IReadOnlyList<ChatMessageContent>> GetChatMessageContentsAsync(ChatHistory chatHistory, PromptExecutionSettings? executionSettings = null,
        Kernel? kernel = null, CancellationToken cancellationToken = new CancellationToken())
    {
        var request = ChatRequest.FromChatHistory(chatHistory, executionSettings);
        
        using var httpClient = new HttpClient();
        
        using var httpResponse = await httpClient.PostAsJsonAsync(
            _options.Endpoint, request, cancellationToken);

        httpResponse.EnsureSuccessStatusCode();

        var response = await httpResponse.Content.ReadFromJsonAsync<ChatResponse>(cancellationToken)
                                ?? throw new Exception("Failed to deserialize response from model");
        
        return response
            .Choices.Select<ChatResponseChoice, ChatMessageContent>(choice =>
                new(AuthorRole.Assistant, choice.Message.Content)
            )
            .ToImmutableList();
    }

    public async IAsyncEnumerable<StreamingChatMessageContent> GetStreamingChatMessageContentsAsync(ChatHistory chatHistory,
        PromptExecutionSettings? executionSettings = null, Kernel? kernel = null,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var request = ChatRequest.FromChatHistory(chatHistory, executionSettings);
        request.Stream = true;
        
        using var httpClient = new HttpClient();
        
        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, _options.Endpoint);
        httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        httpRequest.Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        using var httpResponse = await httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

        httpResponse.EnsureSuccessStatusCode();
       
        var responseStream = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);

        await foreach (var content in System.Net.ServerSentEvents.SseParser.Create(responseStream).EnumerateAsync(cancellationToken))
        {
            yield return new StreamingChatMessageContent(AuthorRole.Assistant, content.Data);
        }
        
    }
}