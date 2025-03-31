using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel.ChatCompletion;
using Void.Chef.Application.Common.Interfaces;
using Void.Chef.Domain.Entities;

namespace Void.Chef.Application.Chef.Commands.CreateChatMessage;

public record CreateChatMessageCommand : IRequest<int>
{
    public int ChatId { get; set; }
    public string Content { get; set; } = null!;
    public AuthorRole AuthorRole { get; set; }
}

public class CreateChatMessageCommandHandler : IRequestHandler<CreateChatMessageCommand, int>
{
    private IApplicationDbContext _context;

    public CreateChatMessageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<int> Handle(CreateChatMessageCommand request, CancellationToken cancellationToken)
    {
        var chat = await _context.Chats.SingleOrDefaultAsync(x => x.Id == request.ChatId, cancellationToken);

        Guard.Against.NotFound(request.ChatId, chat);

        var chatMessage = new ChatMessage()
        {
            Content = request.Content, AuthorRole = request.AuthorRole.Label, Chat = chat
        };
        
        _context.ChatMessages.Add(chatMessage);

        await _context.SaveChangesAsync(cancellationToken);
        
        return chatMessage.Id;
    }
}
