using System.Text.Json;
using System.Text.Json.Nodes;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Void.Chef.Features.AskChef.Shared;
using Void.Chef.Features.Shared.Models;
using Void.Chef.Options;

namespace Void.Chef.Features.AskChef.Queries;

public record GetStreamAnswerQuery : IRequest<IAsyncEnumerable<string>>
{
    public IEnumerable<ChefChatMessage> History { get; set; } = new List<ChefChatMessage>();
}

public class GetStreamAnswerHandler([FromKeyedServices("Chef")] IChatCompletionService chatCompletionService,
    IOptions<ChatCompletionModelOptions> options) : IRequestHandler<GetStreamAnswerQuery, IAsyncEnumerable<string>>
{
    private readonly ChatCompletionModelOptions _options = options.Value;
    
    public Task<IAsyncEnumerable<string>> Handle(GetStreamAnswerQuery request, CancellationToken cancellationToken)
    {
        var execSettings = new PromptExecutionSettings()
        {
            ModelId = _options.Name
        };
        
        var history = new ChatHistory(ChefAIModelParameters.SystemPrompt);
        history.AddRange(request.History
            .Select(m => new ChatMessageContent(m.SentByUser ? AuthorRole.User : AuthorRole.Assistant, m.Content))
        );
        
        var response = chatCompletionService.GetStreamingChatMessageContentsAsync(history,
            execSettings, cancellationToken: cancellationToken);
        
        return Task.FromResult(GetChunkContentAsString(response));
    }

    public async IAsyncEnumerable<string> GetChunkContentAsString(IAsyncEnumerable<StreamingChatMessageContent> chunks)
    {
        
        await foreach (var content in chunks)
        {
            if (content.Content == "[DONE]")
            {
                break;
            }
            
            var choice = JsonSerializer.Deserialize<ChatStreamResponseMessageChunk>(content.Content)?.Choices.FirstOrDefault();

            if (choice.FinishReason != null)
            {
                break;
            }

            yield return choice.Delta.Content;
        }
    }
}

public class GetStreamAnswerValidator : AbstractValidator<GetStreamAnswerQuery>
{
    public GetStreamAnswerValidator()
    {
        RuleFor(x => x.History).NotNull();
    }
}
