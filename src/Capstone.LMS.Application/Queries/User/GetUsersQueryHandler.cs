using Capstone.LMS.Application.Dtos;
using Capstone.LMS.Application.Dtos.User;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.User
{
    public sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ListResponseDto<GetUserResponseDto>>
    {
        public Task<ListResponseDto<GetUserResponseDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
