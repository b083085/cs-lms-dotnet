using Capstone.LMS.Application.Dtos.User;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.User
{
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<CreateUserResponseDto>>
    {
        public Task<Result<CreateUserResponseDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
