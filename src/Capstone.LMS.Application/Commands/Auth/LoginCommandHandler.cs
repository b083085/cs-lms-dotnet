
using Capstone.LMS.Application.Dtos.Auth;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Auth
{
    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponseDto>>
    {
        public Task<Result<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
