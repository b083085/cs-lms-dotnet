using System.Collections.Generic;

namespace Capstone.LMS.Application.Dtos.Dashboard
{
    public record DashboardListDto(
        string Title,
        IEnumerable<string> Items);
}
