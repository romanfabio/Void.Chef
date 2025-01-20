using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Void.Chef.Features.AskChef.Shared;
using Void.Chef.Features.Shared.Models;
using Void.Chef.Options;

namespace Void.Chef.Features.AskChef.Queries;

public record GetAnswerQuery : IRequest<ChefChatMessage>
{
    public IEnumerable<ChefChatMessage> History { get; set; } = new List<ChefChatMessage>();
}

public class GetAnswerHandler([FromKeyedServices("Chef")] IChatCompletionService chatCompletionService,
    IOptions<ChatCompletionModelOptions> options) : IRequestHandler<GetAnswerQuery, ChefChatMessage>
{
    private readonly ChatCompletionModelOptions _options = options.Value;
    
    public async Task<ChefChatMessage> Handle(GetAnswerQuery request, CancellationToken cancellationToken)
    {
        var execSettings = new PromptExecutionSettings()
        {
            ModelId = _options.Name
        };
        
        var history = new ChatHistory(ChefAIModelParameters.SystemPrompt);
        history.AddRange(request.History
            .Select(m => new ChatMessageContent(m.SentByUser ? AuthorRole.User : AuthorRole.Assistant, m.Content))
        );
        
        var response = await chatCompletionService.GetChatMessageContentsAsync(history,
            execSettings, cancellationToken: cancellationToken);
        return new ChefChatMessage() { SentByUser = false, Content = response[0]?.Content ?? "" };
    }
}

public class GetAnswerValidator : AbstractValidator<GetAnswerQuery>
{
    public GetAnswerValidator()
    {
        RuleFor(x => x.History).NotNull();
    }
}
