using Capstone.LMS.Application.Dtos;
using MediatR;
using System;

namespace Capstone.LMS.Application.Commands.Auth
{
    public record RevokeRefreshTokenCommand(
        Guid UserId) : IRequest<SuccessResponseDto>;
}
