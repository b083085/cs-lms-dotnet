using Capstone.LMS.Application.Persistence;
using Capstone.LMS.Domain.Errors;
using Capstone.LMS.Domain.Repositories;
using Capstone.LMS.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Book
{
    public sealed class UpdateBookCommandHandler(
        IBookRepository bookRepository,
        IGenreRepository genreRepository,
        IAuthorRepository authorRepository,
        IUnitOfWork unitOfWork,
        ILogger<UpdateBookCommandHandler> logger) : IRequestHandler<UpdateBookCommand, Result>
    {

        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IGenreRepository _genreRepository = genreRepository;
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<UpdateBookCommandHandler> _logger = logger;

        public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetAsync(b => b.Id == request.BookId, cancellationToken);
            if (book is null)
            {
                return Result.Failure(DomainErrors.Book.NotFound);
            }

            var genre = await _genreRepository.GetAsync(g => g.Id == request.GenreId, cancellationToken);
            if (genre is null)
            {
                return Result.Failure(DomainErrors.Genre.NotFound);
            }

            var author = await _authorRepository.GetAsync(a => a.Id == request.AuthorId, cancellationToken);
            if (author is null)
            {
                return Result.Failure(DomainErrors.Author.NotFound);
            }

            book.SetTitle(request.Title);
            book.SetSummary(request.Summary);
            book.SetIsbn(request.Isbn);
            book.SetPublishedOn(request.PublishedOn);
            book.SetTotalCopies(request.TotalCopies);
            book.SetGenre(genre);
            book.SetAuthor(author);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Book is updated. {BookTitle}", book.Title);

            return Result.Success();
        }
    }
}
