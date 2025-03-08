using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace Void.Chef.Application.Common.Interfaces;

public interface ISemanticKernelService
{
    IAsyncEnumerable<StreamingChatMessageContent> StreamResponse(ChatHistory chatHistory);
}
