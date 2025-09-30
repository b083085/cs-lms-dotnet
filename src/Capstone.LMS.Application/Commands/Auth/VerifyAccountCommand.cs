using Capstone.LMS.Application.Dtos;
using Capstone.LMS.Application.Dtos.Auth;
using Capstone.LMS.Domain.Shared;
using MediatR;

namespace Capstone.LMS.Application.Commands.Auth
{
    public record VerifyAccountCommand(
        string Email) : IRequest<SuccessResponseDto>;
}
