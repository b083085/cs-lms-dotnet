using Capstone.LMS.Application.Dtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Auth
{
    public sealed class VerifyAccountCommandHandler : IRequestHandler<VerifyAccountCommand,SuccessResponseDto>
    {
        public Task<SuccessResponseDto> Handle(VerifyAccountCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
