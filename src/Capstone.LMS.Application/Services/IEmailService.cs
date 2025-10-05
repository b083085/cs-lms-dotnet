using System;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Services
{
    public interface IEmailService
    {
        Task SendEmailConfirmationLinkAsync(Guid userId, CancellationToken cancellationToken);
        Task SendRequestToBorrowBookApprovedEmailAsync(Guid bookBorrowedId, CancellationToken cancellationToken);
        Task SendRequestToBorrowBookRejectedEmailAsync(Guid bookBorrowedId, CancellationToken cancellationToken);
    }
}
