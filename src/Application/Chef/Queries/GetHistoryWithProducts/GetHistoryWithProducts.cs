using MediatR;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Void.Chef.Application.Common.Interfaces;
using Void.Chef.Application.Products.Queries.GetProducts;

namespace Void.Chef.Application.Chef.Queries.AskChefWithProducts;

public record GetHistoryWithProductsQuery() : IRequest<ChatHistory>;

public class
    AskChefWithProductsQueryHandler : IRequestHandler<GetHistoryWithProductsQuery,
    ChatHistory>
{
    private readonly ISender _sender;

    public AskChefWithProductsQueryHandler(ISender sender)
    {
        _sender = sender;
    }
    
    public async Task<ChatHistory> Handle(GetHistoryWithProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _sender.Send(new GetProductsQuery(), cancellationToken);

        var chatHistory = new ChatHistory($@"
You are an assistant chef who helps people with cooking recipes.
Below you are listed the ingredients the user has available.
Only respond with recipes that are possible with the ingredients available.

{string.Join('\n',products.Select(p => $"- {p.Name}"))}
        ");
        
        return chatHistory;
    }
}
