using Capstone.LMS.Application.Services;

namespace Capstone.LMS.Infrastructure.Services
{
    internal sealed class EmailService : IEmailService
    {
        public Task SendAsync()
        {
            return Task.CompletedTask;
        }
    }
}
