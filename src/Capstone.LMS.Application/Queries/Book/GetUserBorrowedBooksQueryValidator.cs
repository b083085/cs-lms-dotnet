using FluentValidation;

namespace Capstone.LMS.Application.Queries.Book
{
    public sealed class GetUserBorrowedBooksQueryValidator : AbstractValidator<GetUserBorrowedBooksQuery>
    {
        public GetUserBorrowedBooksQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
