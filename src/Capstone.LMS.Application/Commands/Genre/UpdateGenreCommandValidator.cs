using FluentValidation;

namespace Capstone.LMS.Application.Commands.Genre
{
    public sealed class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(x => x.BookId).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Summary).NotEmpty();
            RuleFor(x => x.Isbn).NotEmpty();
            RuleFor(x => x.PublishedOn).NotEmpty();
            RuleFor(x => x.TotalCopies).NotEmpty();
            RuleFor(x => x.GenreId).NotEmpty();
            RuleFor(x => x.AuthorId).NotEmpty();
        }
    }
}
