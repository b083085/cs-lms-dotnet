using Capstone.LMS.Application.Dtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.User
{
    public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, SuccessResponseDto>
    {
        public Task<SuccessResponseDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
