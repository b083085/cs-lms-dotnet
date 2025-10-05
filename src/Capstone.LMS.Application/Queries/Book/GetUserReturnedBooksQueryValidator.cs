using FluentValidation;

namespace Capstone.LMS.Application.Queries.Book
{
    public sealed class GetUserReturnedBooksQueryValidator : AbstractValidator<GetUserReturnedBooksQuery>
    {
        public GetUserReturnedBooksQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
