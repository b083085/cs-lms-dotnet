using Capstone.LMS.Application.Dtos;
using MediatR;

namespace Capstone.LMS.Application.Commands.Auth
{
    public record LogoutCommand(
        string Email,
        string SessionId) : IRequest<SuccessResponseDto>;
}
