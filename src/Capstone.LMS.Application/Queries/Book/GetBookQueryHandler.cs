using Capstone.LMS.Application.Dtos.Book;
using Capstone.LMS.Domain.Errors;
using Capstone.LMS.Domain.Repositories;
using Capstone.LMS.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.Book
{
    public sealed class GetBookQueryHandler(
        IBookRepository bookRepository,
        ILogger<GetBookQueryHandler> logger)
        : IRequestHandler<GetBookQuery, Result<GetBookResponseDto>>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly ILogger<GetBookQueryHandler> _logger = logger;

        public async Task<Result<GetBookResponseDto>> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.BookId, cancellationToken);
            if(book is null)
            {
                return Result.Failure<GetBookResponseDto>(DomainErrors.Book.BookNotFound);
            }

            return new GetBookResponseDto
            {
                BookId = book.Id,
                Title = book.Title,
                Summary = book.Summary,
                Isbn = book.Isbn,
                PublishedOn = book.PublishedOn,
                TotalCopies = book.TotalCopies,
                Availability = book.Availability,
                Genre = book.Genre is null ? null : new Dtos.Genre.GetGenreResponseDto
                {
                    GenreId = book.Genre.Id,
                    Name = book.Genre.Name
                },
                Author = book.Author is null ? null : new Dtos.Author.GetAuthorResponseDto
                {
                    AuthorId = book.Author.Id,
                    Name = book.Author.Name
                }
            };
        }
    }
}
