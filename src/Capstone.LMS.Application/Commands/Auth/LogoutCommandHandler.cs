using Capstone.LMS.Application.Dtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Auth
{
    public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, SuccessResponseDto>
    {
        public Task<SuccessResponseDto> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
