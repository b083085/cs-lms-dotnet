using FluentValidation;

namespace Capstone.LMS.Application.Queries.Book
{
    public sealed class GetBookBorrowedQueryValidator : AbstractValidator<GetBookBorrowedQuery>
    {
        public GetBookBorrowedQueryValidator()
        {
            RuleFor(x => x.BookBorrowedId).NotEmpty();
        }
    }
}
