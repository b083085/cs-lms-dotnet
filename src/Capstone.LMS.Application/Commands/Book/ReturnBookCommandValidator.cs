using FluentValidation;

namespace Capstone.LMS.Application.Commands.Book
{
    public sealed class ReturnBookCommandValidator : AbstractValidator<ReturnBookCommand>
    {
        public ReturnBookCommandValidator()
        {
            RuleFor(x => x.BookId).NotEmpty();
        }
    }
}
