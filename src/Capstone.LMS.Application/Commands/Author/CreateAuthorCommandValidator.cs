using FluentValidation;

namespace Capstone.LMS.Application.Commands.Author
{
    public sealed class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
