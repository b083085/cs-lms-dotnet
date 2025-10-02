using Capstone.LMS.Application.Services;
using Capstone.LMS.Domain.DomainEvents;
using Capstone.LMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Events.Auth
{
    internal sealed class UserRegisteredDomainEventHandler(
        IEmailService emailService, UserManager<User> userManager) 
        : INotificationHandler<UserRegisteredDomainEvent>
    {
        private readonly IEmailService _emailService = emailService;
        private readonly UserManager<User> _userManager = userManager;

        public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(notification.UserId.ToString());
            if(user is null)
            {
                return;
            }

            await _emailService.SendAsync();
        }
    }
}
