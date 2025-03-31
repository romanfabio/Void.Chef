using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel.ChatCompletion;
using Void.Chef.Application.Common.Interfaces;

namespace Void.Chef.Application.Chef.Queries.GetChatById;

public record GetChatByIdQuery(int Id) : IRequest<ChatHistory>;

public class GetChatByIdQueryHandler : IRequestHandler<GetChatByIdQuery, ChatHistory>
{
    private readonly IApplicationDbContext _context;

    public GetChatByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<ChatHistory> Handle(GetChatByIdQuery request, CancellationToken cancellationToken)
    {
        var chat = await _context.Chats.AsNoTracking()
            .Include(x => x.Messages)
            .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, chat);

        var history = new ChatHistory();

        foreach (var message in chat.Messages.OrderBy(x => x.SentAt))
        {
            if (message.AuthorRole == AuthorRole.System.Label)
            {
                history.AddSystemMessage(message.Content);
            } else if (message.AuthorRole == AuthorRole.Assistant.Label)
            {
                history.AddAssistantMessage(message.Content);
            } else if (message.AuthorRole == AuthorRole.User.Label)
            {
                history.AddUserMessage(message.Content);
            }
        }

        return history;
    }
}
