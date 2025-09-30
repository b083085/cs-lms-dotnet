using Capstone.LMS.Application.Dtos.User;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.User
{
    public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<UpdateUserResponseDto>>
    {
        public Task<Result<UpdateUserResponseDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
