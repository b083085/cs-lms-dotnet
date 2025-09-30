using Capstone.LMS.Application.Dtos.Auth;
using Capstone.LMS.Domain.Shared;
using MediatR;

namespace Capstone.LMS.Application.Commands.Auth
{
    public record LoginCommand(
        string Email,
        string Password) : IRequest<Result<LoginResponseDto>>;
}
