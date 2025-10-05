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
    public sealed class GetBookBorrowedQueryHandler(
        IBorrowedBookRepository borrowedBookRepository,
        IUserRepository userRepository,
        ILogger<GetBookBorrowedQueryHandler> logger)
        : IRequestHandler<GetBookBorrowedQuery, Result<GetBookBorrowedResponseDto>>
    {
        private readonly IBorrowedBookRepository _borrowedBookRepository = borrowedBookRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<GetBookBorrowedQueryHandler> _logger = logger;

        public async Task<Result<GetBookBorrowedResponseDto>> Handle(GetBookBorrowedQuery request, CancellationToken cancellationToken)
        {
            var bookBorrowed = await _borrowedBookRepository.GetAsync(b => b.Id == request.BookBorrowedId, cancellationToken);
            if (bookBorrowed is null)
            {
                return Result.Failure<GetBookBorrowedResponseDto>(DomainErrors.BookBorrowed.NotFound);
            }

            var approver = await _userRepository.GetAsync(u => u.Id == bookBorrowed.ApprovedBy, cancellationToken);

            var book = bookBorrowed.Book;

            return new GetBookBorrowedResponseDto
            {
                BookBorrowedId = bookBorrowed.Id,
                Status = bookBorrowed.Status,
                ApproverName = approver?.GetFullName(),
                ApprovedOnUtc = bookBorrowed.ApprovedOnUtc,
                BorrowedOnUtc = bookBorrowed.BorrowedOnUtc,
                DueOnUtc = bookBorrowed.DueOnUtc,
                ReturnedOnUtc = bookBorrowed.ReturnedOnUtc,
                Book = new GetBookItemResponseDto
                {
                    BookId = book.Id,
                    Title = book.Title,
                    Summary = book.Summary,
                    Isbn = book.Isbn,
                    PublishedOn = book.PublishedOn,
                    TotalCopies = book.TotalCopies,
                    Availability = book.Availability,
                    Genre = book.Genre == null ? null : new Dtos.Genre.GetGenreResponseDto
                    {
                        GenreId = book.Genre.Id,
                        Name = book.Genre.Name
                    },
                    Author = book.Author == null ? null : new Dtos.Author.GetAuthorResponseDto
                    {
                        AuthorId = book.Author.Id,
                        Name = book.Author.Name
                    }
                }
            };
        }
    }
}
