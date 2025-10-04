using FluentValidation;

namespace Capstone.LMS.Application.Commands.Author
{
    public sealed class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(x => x.AuthorId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
