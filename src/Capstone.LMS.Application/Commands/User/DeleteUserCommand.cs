using Capstone.LMS.Application.Dtos;
using MediatR;
using System;

namespace Capstone.LMS.Application.Commands.User
{
    public record DeleteUserCommand(
        Guid UserId) : IRequest<SuccessResponseDto>;
}
