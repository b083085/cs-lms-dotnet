using Capstone.LMS.Application.Services;
using Capstone.LMS.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Events.Book
{
    internal sealed class RejectedBorrowBookDomainEventHandler(
        IEmailService emailService,
        ILogger<RejectedBorrowBookDomainEventHandler> logger) 
        : INotificationHandler<RejectedBorrowBookDomainEvent>
    {
        private readonly IEmailService _emailService = emailService;
        private readonly ILogger<RejectedBorrowBookDomainEventHandler> _logger = logger;

        public async Task Handle(RejectedBorrowBookDomainEvent notification, CancellationToken cancellationToken)
        {
            await _emailService.SendRequestToBorrowBookRejectedEmailAsync(notification.BookBorrowedId, cancellationToken);
        }
    }
}
