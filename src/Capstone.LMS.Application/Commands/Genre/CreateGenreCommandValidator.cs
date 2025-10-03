using FluentValidation;

namespace Capstone.LMS.Application.Commands.Genre
{
    public sealed class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
