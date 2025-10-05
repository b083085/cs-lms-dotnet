using Capstone.LMS.Application.Services;
using Capstone.LMS.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Events.Book
{
    internal sealed class ApprovedBorrowBookDomainEventHandler(
        IEmailService emailService,
        ILogger<ApprovedBorrowBookDomainEventHandler> logger) 
        : INotificationHandler<ApprovedBorrowBookDomainEvent>
    {
        private readonly IEmailService _emailService = emailService;
        private readonly ILogger<ApprovedBorrowBookDomainEventHandler> _logger = logger;

        public async Task Handle(ApprovedBorrowBookDomainEvent notification, CancellationToken cancellationToken)
        {
            await _emailService.SendRequestToBorrowBookApprovedEmailAsync(notification.BookBorrowedId, cancellationToken);
        }
    }
}
