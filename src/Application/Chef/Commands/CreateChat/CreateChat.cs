using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel.ChatCompletion;
using Void.Chef.Application.Common.Interfaces;
using Void.Chef.Application.Products.Queries.GetProducts;
using Void.Chef.Domain.Constants;
using Void.Chef.Domain.Entities;

namespace Void.Chef.Application.Chef.Commands.CreateChat;

public record CreateChatCommand : IRequest<int>;

public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, int>
{
    private readonly ISender _sender;
    private readonly IApplicationDbContext _context;

    public CreateChatCommandHandler(ISender sender, IApplicationDbContext context)
    {
        _sender = sender;
        _context = context;
    }

    public async Task<int> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        var products = await _context.Products.AsNoTracking().ToListAsync(cancellationToken);
        
        var systemMessage = SystemMessages.BuildWithProducts(products);

        var chat = new Chat();
        chat.Messages.Add(new ChatMessage() { AuthorRole = AuthorRole.System.ToString(), Content = systemMessage, SentAt = DateTime.Now});

        _context.Chats.Add(chat);

        await _context.SaveChangesAsync(cancellationToken);

        return chat.Id;
    }
}
