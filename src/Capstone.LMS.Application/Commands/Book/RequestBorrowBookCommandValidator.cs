using FluentValidation;

namespace Capstone.LMS.Application.Commands.Book
{
    public sealed class RequestBorrowBookCommandValidator : AbstractValidator<RequestBorrowBookCommand>
    {
        public RequestBorrowBookCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.BookId).NotEmpty();
        }
    }
}
