using Capstone.LMS.Application.Dtos.Dashboard;
using MediatR;

namespace Capstone.LMS.Application.Queries.Dashboard
{
    public record GetDashboardQuery(
        string Role) : IRequest<GetDashboardResponseDto>;
}
