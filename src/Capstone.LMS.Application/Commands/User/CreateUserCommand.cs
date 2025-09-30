using Capstone.LMS.Application.Dtos.User;
using Capstone.LMS.Domain.Shared;
using MediatR;

namespace Capstone.LMS.Application.Commands.User
{
    public record CreateUserCommand(
        string FirstName,
        string LastName,
        string Gender,
        string Email) : IRequest<Result<CreateUserResponseDto>>;
}
