using Capstone.LMS.Application.Dtos.Dashboard;
using Capstone.LMS.Application.Queries.Dashboard;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Capstone.LMS.Presentation.Endpoints
{
    public class DashboardEndpoints : BaseEndpoints, ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = CreateMapGroup(app, "dashboard")
                .WithTags("Dashboard");

            group.MapGet("{role}", GetDashboardAsync)
                 .WithName("GetDashboard")
                 .WithSummary("Gets the dashboard components by role.");
        }

        private static async Task<Ok<GetDashboardResponseDto>> GetDashboardAsync(
            IMediator mediator,
            string role,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetDashboardQuery(role), cancellationToken);

            return TypedResults.Ok(result);
        }
    }
}
