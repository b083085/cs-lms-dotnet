using Capstone.LMS.Application.Persistence;
using Capstone.LMS.Domain.DomainEvents;
using Capstone.LMS.Domain.Exceptions;
using Capstone.LMS.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Events.Book
{
    internal sealed class BorrowedBookDomainEventHandler(
        IBookRepository bookRepository,
        IUnitOfWork unitOfWork,
        ILogger<BorrowedBookDomainEventHandler> logger) 
        : INotificationHandler<BorrowedBookDomainEvent>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<BorrowedBookDomainEventHandler> _logger = logger;

        public async Task Handle(BorrowedBookDomainEvent notification, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(notification.BookId, cancellationToken);
            if (book is null)
            {
                throw new BookNotFoundException(notification.BookId);
            }

            book.UpdateAvailability();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Book availability has been updated. {BookId}", book.Id);
        }
    }
}
