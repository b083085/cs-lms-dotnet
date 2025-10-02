using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Email
{
    public interface IEmailClient
    {
        Task SendEmailAsync(EmailMessage emailMessage, CancellationToken cancellationToken);
    }
}
