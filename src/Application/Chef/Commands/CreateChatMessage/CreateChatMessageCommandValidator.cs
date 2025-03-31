using FluentValidation;

namespace Void.Chef.Application.Chef.Commands.CreateChatMessage;

public class CreateChatMessageCommandValidator : AbstractValidator<CreateChatMessageCommand>
{
    public CreateChatMessageCommandValidator()
    {
        RuleFor(x => x.Content).NotEmpty();
        RuleFor(x => x.ChatId).GreaterThan(0);
    }
}
