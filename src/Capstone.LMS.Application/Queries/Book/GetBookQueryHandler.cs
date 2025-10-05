using Capstone.LMS.Application.Dtos.Author;
using Capstone.LMS.Application.Dtos.Book;
using Capstone.LMS.Application.Dtos.Genre;
using Capstone.LMS.Application.Dtos.User;
using Capstone.LMS.Domain.Errors;
using Capstone.LMS.Domain.Repositories;
using Capstone.LMS.Domain.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
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
            var query = _bookRepository.GetQueryable();

            var book = await query
                        .Include(q => q.Genre)
                        .Include(q => q.Author)
                        .Include(q => q.BorrowedBooks).ThenInclude(q => q.User)
                        .Where(q => q.Id == request.BookId)
                        .Select(book => new GetBookResponseDto
                        {
                            BookId = book.Id,
                            Title = book.Title,
                            Summary = book.Summary,
                            Isbn = book.Isbn,
                            PublishedOn = book.PublishedOn,
                            TotalCopies = book.TotalCopies,
                            Availability = book.Availability,
                            Genre = book.Genre == null ? null : new GetGenreResponseDto
                            {
                                GenreId = book.Genre.Id,
                                Name = book.Genre.Name
                            },
                            Author = book.Author == null ? null : new GetAuthorResponseDto
                            {
                                AuthorId = book.Author.Id,
                                Name = book.Author.Name
                            },
                            Borrowers = book.BorrowedBooks.Any() ? 
                            book.BorrowedBooks.Select(borrow => new GetBorrowedBookStatusResponseDto
                            {
                                Borrower = new GetUserResponseDto
                                {
                                    UserId = borrow.User.Id,
                                    UserName = borrow.User.UserName,
                                    Email = borrow.User.Email,
                                    FirstName = borrow.User.FirstName,
                                    LastName = borrow.User.LastName,
                                    Gender = borrow.User.Gender.Value
                                },
                                DueOnUtc = borrow.DueOnUtc
                            }) :
                            Enumerable.Empty<GetBorrowedBookStatusResponseDto>()
                        })
                        .FirstOrDefaultAsync(cancellationToken);

            if(book is null)
            {
                return Result.Failure<GetBookResponseDto>(DomainErrors.Book.BookNotFound);
            }

            return book;
        }
    }
}
