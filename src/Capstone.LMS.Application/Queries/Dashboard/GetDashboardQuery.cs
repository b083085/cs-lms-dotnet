using Capstone.LMS.Application.Dtos.Dashboard;
using MediatR;
using System;

namespace Capstone.LMS.Application.Queries.Dashboard
{
    public record GetDashboardQuery(
        Guid UserId,
        string Role) : IRequest<GetDashboardResponseDto>;
}
