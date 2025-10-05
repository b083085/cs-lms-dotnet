using FluentValidation;

namespace Capstone.LMS.Application.Commands.Book
{
    public sealed class ApproveBorrowBookCommandValidator : AbstractValidator<ApproveBorrowBookCommand>
    {
        public ApproveBorrowBookCommandValidator()
        {
            RuleFor(x => x.BookBorrowedId).NotEmpty();
            RuleFor(x => x.Approve).NotEmpty();

            RuleFor(x => x.RejectReason)
                .NotEmpty()
                .When(x => x.Approve == false)
                .WithMessage("Reject reason is required when rejected.");
        }
    }
}
