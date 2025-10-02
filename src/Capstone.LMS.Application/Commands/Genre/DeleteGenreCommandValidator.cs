using FluentValidation;

namespace Capstone.LMS.Application.Commands.Genre
{
    public sealed class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(x => x.BookId).NotEmpty();
        }
    }
}
