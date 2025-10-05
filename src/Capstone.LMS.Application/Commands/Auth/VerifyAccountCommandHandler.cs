using Capstone.LMS.Domain.Errors;
using Capstone.LMS.Domain.Extensions;
using Capstone.LMS.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Auth
{
    public sealed class VerifyAccountCommandHandler(
        UserManager<Domain.Entities.User> userManager,
        ILogger<VerifyAccountCommandHandler> logger
        ) : 
        IRequestHandler<VerifyAccountCommand, Result>
    {
        private readonly UserManager<Domain.Entities.User> _userManager = userManager;
        private readonly ILogger<VerifyAccountCommandHandler> _logger = logger;

        public async Task<Result> Handle(VerifyAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user is null)
            {
                return Result.Failure(DomainErrors.User.NotFound);
            }

            if (user.EmailConfirmed)
            {
                return Result.Failure(DomainErrors.User.AlreadyConfirmed);
            }

            var decodedEmailConfirmationToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            
            var confirmEmailResult = await _userManager.ConfirmEmailAsync(user, decodedEmailConfirmationToken);
            if (!confirmEmailResult.Succeeded)
            {
                return confirmEmailResult.Failure();
            }

            return Result.Success();
        }
    }
}
