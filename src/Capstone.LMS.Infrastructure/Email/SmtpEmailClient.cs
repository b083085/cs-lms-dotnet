using Capstone.LMS.Application.Email;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Capstone.LMS.Infrastructure.Email
{
    internal sealed class SmtpEmailClient(IOptions<SmtpEmailOptions> smtpOptions) : IEmailClient
    {
        private readonly SmtpEmailOptions _smtpOptions = smtpOptions.Value;

        public async Task SendEmailAsync(EmailMessage emailMessage, CancellationToken cancellationToken)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpOptions.EmailAddress)
            };
            mailMessage.To.Add(emailMessage.To);
            mailMessage.Subject = emailMessage.Subject;
            mailMessage.Body = emailMessage.Body; 
            mailMessage.IsBodyHtml = true;

            using var smtpClient = new SmtpClient(_smtpOptions.Server, _smtpOptions.Port)
            {
                Credentials = new NetworkCredential(_smtpOptions.EmailAddress, _smtpOptions.Password),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(mailMessage, cancellationToken);
        }
    }
}
