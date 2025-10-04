using FluentValidation;

namespace Capstone.LMS.Application.Commands.Author
{
    public sealed class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(x => x.AuthorId).NotEmpty();
        }
    }
}
