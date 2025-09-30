using FluentValidation;

namespace Capstone.LMS.Application.Queries.Book
{
    public sealed class GetBookQueryValidator : AbstractValidator<GetBookQuery>
    {
        public GetBookQueryValidator()
        {
            RuleFor(x => x.BookId).NotEmpty();
        }
    }
}
