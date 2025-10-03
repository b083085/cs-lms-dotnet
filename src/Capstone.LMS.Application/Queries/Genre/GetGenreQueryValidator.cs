using FluentValidation;

namespace Capstone.LMS.Application.Queries.Genre
{
    public sealed class GetGenreQueryValidator : AbstractValidator<GetGenreQuery>
    {
        public GetGenreQueryValidator()
        {
            RuleFor(x => x.GenreId).NotEmpty();
        }
    }
}
