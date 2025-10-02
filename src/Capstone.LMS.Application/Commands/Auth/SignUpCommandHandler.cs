using Capstone.LMS.Domain.Constants;
using Capstone.LMS.Domain.Extensions;
using Capstone.LMS.Domain.Shared;
using Capstone.LMS.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Auth
{
    public sealed class SignUpCommandHandler(
        UserManager<Domain.Entities.User> userManager,
        ILogger<SignUpCommandHandler> logger)
        : IRequestHandler<SignUpCommand, Result>
    {
        private readonly UserManager<Domain.Entities.User> _userManager = userManager;
        private readonly ILogger<SignUpCommandHandler> _logger = logger;

        public async Task<Result> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var gender = Gender.Create(request.Gender);
            if (gender.IsFailure)
            {
                return Result.Failure(gender.Error);
            }

            var user = Domain.Entities.User.Create(
                Guid.NewGuid(),
                request.FirstName,
                request.LastName,
                gender.Value,
                request.Email);

            var createUserResult = await _userManager.CreateAsync(user, request.Password);
            if (!createUserResult.Succeeded)
            {
                return createUserResult.Failure();
            }

            _logger.LogInformation("User is created. {@User}", user);

            var addUserRoleResult = await _userManager.AddToRoleAsync(user, Roles.Borrower);
            if (!addUserRoleResult.Succeeded)
            {
                return addUserRoleResult.Failure();
            }

            return Result.Success();
        }
    }
}
