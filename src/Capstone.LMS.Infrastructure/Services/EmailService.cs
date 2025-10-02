using Capstone.LMS.Application.Services;
using Microsoft.Extensions.Logging;

namespace Capstone.LMS.Infrastructure.Services
{
    internal sealed class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public Task SendAsync()
        {
            _logger.LogInformation("Email sent.");
            return Task.CompletedTask;
        }
    }
}
