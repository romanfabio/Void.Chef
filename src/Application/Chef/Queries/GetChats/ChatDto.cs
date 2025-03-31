using AutoMapper;
using Microsoft.SemanticKernel.ChatCompletion;
using Void.Chef.Domain.Entities;

namespace Void.Chef.Application.Chef.Queries.GetChats;

public class ChatDto
{
    public int Id { get; set; }
    
    public string? LastUserMessageContent { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Chat, ChatDto>()
                .ForMember(x => x.LastUserMessageContent, o => o.MapFrom(
                    s => s.Messages.OrderByDescending(x => x.SentAt)
                        .FirstOrDefault(x => x.AuthorRole == AuthorRole.User.Label)));
        }
    }
}
