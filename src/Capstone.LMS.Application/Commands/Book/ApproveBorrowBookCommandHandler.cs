using Capstone.LMS.Application.Extensions;
using Capstone.LMS.Application.Persistence;
using Capstone.LMS.Domain.Errors;
using Capstone.LMS.Domain.Repositories;
using Capstone.LMS.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Book
{
    public sealed class ApproveBorrowBookCommandHandler(
        IBorrowedBookRepository borrowedBookRepository,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        ILogger<ApproveBorrowBookCommandHandler> logger
        ) : IRequestHandler<ApproveBorrowBookCommand, Result>
    {
        private readonly IBorrowedBookRepository _borrowedBookRepository = borrowedBookRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly ILogger<ApproveBorrowBookCommandHandler> _logger = logger;

        public async Task<Result> Handle(ApproveBorrowBookCommand request, CancellationToken cancellationToken)
        {
            var bookBorrowed = await _borrowedBookRepository.GetAsync(b => b.Id == request.BookBorrowedId, cancellationToken);
            if (bookBorrowed is null)
            {
                return Result.Failure(DomainErrors.BookBorrowed.NotFound);
            }

            var approverId = _httpContextAccessor.GetCurrentUserId();

            if (request.Approve)
            {
                if (!bookBorrowed.Book.IsAvailable())
                {
                    return Result.Failure(DomainErrors.Book.IsUnavailable);
                }

                bookBorrowed.Approve(approverId);
            }
            else
            {
                bookBorrowed.Rejected(approverId, request.RejectReason);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("The request by {FirstName} {LastName} to borrow the book {BookTitle} has been {ApproveStatus}.", 
                bookBorrowed.Book.Title, 
                bookBorrowed.User.FirstName, 
                bookBorrowed.User.LastName,
                bookBorrowed.Status.ToString());

            return Result.Success();
        }
    }
}
