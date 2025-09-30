using FluentValidation;

namespace Capstone.LMS.Application.Commands.Book
{
    public sealed class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(x => x.BookId).NotEmpty();
        }
    }
}
