using Capstone.LMS.Application.Dtos.User;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.User
{
    public sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<GetUserResponseDto>>
    {
        public Task<Result<GetUserResponseDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
