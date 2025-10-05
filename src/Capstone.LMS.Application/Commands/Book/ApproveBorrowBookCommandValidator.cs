using FluentValidation;

namespace Capstone.LMS.Application.Commands.Book
{
    public sealed class ApproveBorrowBookCommandValidator : AbstractValidator<ApproveBorrowBookCommand>
    {
        public ApproveBorrowBookCommandValidator()
        {
            RuleFor(x => x.BookBorrowedId).NotEmpty();
            RuleFor(x => x.Approve).NotEmpty();
        }
    }
}
