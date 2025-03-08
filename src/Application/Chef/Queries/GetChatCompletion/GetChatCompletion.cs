using MediatR;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Void.Chef.Application.Common.Interfaces;

namespace Void.Chef.Application.Chef.Queries.GetChatCompletion;

public record GetChatCompletionQuery(ChatHistory History) : IRequest<IAsyncEnumerable<StreamingChatMessageContent>>;

public class GetChatCompletionQueryHandler
    : IRequestHandler<GetChatCompletionQuery, IAsyncEnumerable<StreamingChatMessageContent>>
{
    private ISemanticKernelService _semanticKernelService;

    public GetChatCompletionQueryHandler(ISemanticKernelService semanticKernelService)
    {
        _semanticKernelService = semanticKernelService;
    }
    public Task<IAsyncEnumerable<StreamingChatMessageContent>> Handle(GetChatCompletionQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_semanticKernelService.StreamResponse(request.History));
    }
}
