using Microsoft.Extensions.Options;
using Void.Chef.Application.Common.Interfaces;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using OllamaSharp;
using OllamaSharp.Models.Chat;

namespace Void.Chef.Infrastructure.SemanticKernel;

public class OllamaSemanticKernelService : ISemanticKernelService
{
    private readonly OllamaApiClient _apiClient;
    
    public OllamaSemanticKernelService(IOptions<OllamaSemanticKernelSettings> options)
    {
        _apiClient = new OllamaApiClient(options.Value.Endpoint, options.Value.DefaultModelId);
    }

    public IAsyncEnumerable<StreamingChatMessageContent> StreamResponse(ChatHistory chatHistory)
    {
#pragma warning disable SKEXP0001
        var chatService = _apiClient.AsChatCompletionService();
#pragma warning restore SKEXP0001

        return chatService.GetStreamingChatMessageContentsAsync(chatHistory);
    }
    
  
}
