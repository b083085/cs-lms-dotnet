using Capstone.LMS.Application.Dtos.Auth;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Auth
{
    public sealed class SignUpCommandHandler : IRequestHandler<SignUpCommand, Result<SignUpResponseDto>>
    {
        public Task<Result<SignUpResponseDto>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
