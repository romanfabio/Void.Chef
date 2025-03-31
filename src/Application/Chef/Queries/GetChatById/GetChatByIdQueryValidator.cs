using FluentValidation;

namespace Void.Chef.Application.Chef.Queries.GetChatById;

public class GetChatByIdQueryValidator : AbstractValidator<GetChatByIdQuery>
{
    public GetChatByIdQueryValidator()
    {
        RuleFor(q => q.Id).GreaterThan(0);
    }
}
