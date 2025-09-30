using FluentValidation;

namespace Capstone.LMS.Application.Commands.Book
{
    public sealed class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
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
