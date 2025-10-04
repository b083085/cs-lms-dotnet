using FluentValidation;

namespace Capstone.LMS.Application.Queries.Book
{
    public sealed class GetBooksQueryValidator : AbstractValidator<GetBooksQuery>
    {
        public GetBooksQueryValidator()
        {
            RuleFor(x => x.Page).NotEmpty();
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage("The minimum page number is 1.");
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).WithMessage("The minimum page size is 1.");
        }
    }
}
