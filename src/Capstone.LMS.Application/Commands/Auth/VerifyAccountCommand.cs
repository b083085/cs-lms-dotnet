using Capstone.LMS.Domain.Shared;
using MediatR;
using System;

namespace Capstone.LMS.Application.Commands.Auth
{
    public record VerifyAccountCommand(
        Guid UserId,
        string Token) : IRequest<Result>;
}
