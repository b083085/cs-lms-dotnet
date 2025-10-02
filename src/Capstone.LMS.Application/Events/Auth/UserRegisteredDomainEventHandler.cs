using Capstone.LMS.Application.Services;
using Capstone.LMS.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Events.Auth
{
    internal sealed class UserRegisteredDomainEventHandler(
        IEmailService emailService,
        ILogger<UserRegisteredDomainEventHandler> logger) 
        : INotificationHandler<UserRegisteredDomainEvent>
    {
        private readonly IEmailService _emailService = emailService;
        private readonly ILogger<UserRegisteredDomainEventHandler> _logger = logger;

        public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            await _emailService.SendEmailConfirmationLinkAsync(notification.UserId, cancellationToken);
        }
    }
}
