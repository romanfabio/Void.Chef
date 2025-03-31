using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel.ChatCompletion;
using Void.Chef.Application.Common.Interfaces;

namespace Void.Chef.Application.Chef.Queries.GetChats;

public record GetChatsQuery : IRequest<IEnumerable<ChatDto>>;

public class GetChatsQueryHandler : IRequestHandler<GetChatsQuery, IEnumerable<ChatDto>>
{
    private IApplicationDbContext _context;
    private IMapper _mapper;

    public GetChatsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ChatDto>> Handle(GetChatsQuery request, CancellationToken cancellationToken)
    {
        var chats = await _context.Chats.ToListAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<ChatDto>>(chats);
    }
}
