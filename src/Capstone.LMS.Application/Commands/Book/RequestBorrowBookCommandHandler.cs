using Capstone.LMS.Application.Dtos.Book;
using Capstone.LMS.Application.Dtos.User;
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
    public sealed class RequestBorrowBookCommandHandler(
        IBookRepository bookRepository,
        IUserRepository userRepository,
        IBorrowedBookRepository borrowedBookRepository,
        IUnitOfWork unitOfWork,
        ILogger<RequestBorrowBookCommandHandler> logger
        ) : IRequestHandler<RequestBorrowBookCommand, Result<BorrowBookResponseDto>>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IBorrowedBookRepository _borrowedBookRepository = borrowedBookRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<RequestBorrowBookCommandHandler> _logger = logger;

        public async Task<Result<BorrowBookResponseDto>> Handle(RequestBorrowBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetAsync(p => p.Id == request.BookId, cancellationToken);
            if (book is null)
            {
                return Result.Failure<BorrowBookResponseDto>(DomainErrors.Book.NotFound);
            }

            var user = await _userRepository.GetAsync(p => p.Id == request.UserId, cancellationToken);
            if (user is null)
            {
                return Result.Failure<BorrowBookResponseDto>(DomainErrors.User.NotFound);
            }

            if (!book.IsAvailable())
            {
                return Result.Failure<BorrowBookResponseDto>(DomainErrors.Book.IsUnavailable);
            }

            var bookBorrowed = book.Request(user);

            await _borrowedBookRepository.CreateAsync(bookBorrowed, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("{FirstName} {LastName} requested to borrow the book {BookTitle} has been created.", 
                user.FirstName, 
                user.LastName, 
                book.Title);

            return new BorrowBookResponseDto
            {
                BookBorrowedId = bookBorrowed.Id,
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
                },
                User = new GetUserResponseDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender.Value
                }
            };
        }
    }
}
