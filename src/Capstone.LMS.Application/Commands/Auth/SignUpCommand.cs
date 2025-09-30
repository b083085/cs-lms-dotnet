using Capstone.LMS.Application.Dtos.Auth;
using Capstone.LMS.Domain.Shared;
using MediatR;

namespace Capstone.LMS.Application.Commands.Auth
{
    public record SignUpCommand(
        string FirstName,
        string LastName,
        string Gender,
        string Email,
        string Password,
        string ConfirmPassword) : IRequest<Result<SignUpResponseDto>>;
}
