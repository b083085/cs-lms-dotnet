using Capstone.LMS.Application.Dtos.User;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System;

namespace Capstone.LMS.Application.Commands.User
{
    public record UpdateUserCommand(
        Guid UserId,
        string FirstName,
        string LastName,
        string Gender,
        string Email) : IRequest<Result<UpdateUserResponseDto>>;
}
