using Capstone.LMS.Application.Dtos.Dashboard;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.Dashboard
{
    public sealed class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, GetDashboardResponseDto>
    {
        public Task<GetDashboardResponseDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
