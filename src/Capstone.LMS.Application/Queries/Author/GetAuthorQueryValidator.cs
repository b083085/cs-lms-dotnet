using FluentValidation;

namespace Capstone.LMS.Application.Queries.Author
{
    public sealed class GetAuthorQueryValidator : AbstractValidator<GetAuthorQuery>
    {
        public GetAuthorQueryValidator()
        {
            RuleFor(x => x.AuthorId).NotEmpty();
        }
    }
}
