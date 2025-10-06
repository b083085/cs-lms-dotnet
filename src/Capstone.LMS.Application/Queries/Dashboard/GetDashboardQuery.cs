using Capstone.LMS.Application.Dtos.Dashboard;
using MediatR;
using System;

namespace Capstone.LMS.Application.Queries.Dashboard
{
    public record GetDashboardQuery() : IRequest<GetDashboardResponseDto>
    {
        public Guid? UserId { get; init; }
        public string Role { get; init; }
    }
}
