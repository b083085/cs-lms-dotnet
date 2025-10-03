using FluentValidation;

namespace Capstone.LMS.Application.Commands.Genre
{
    public sealed class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(x => x.GenreId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
