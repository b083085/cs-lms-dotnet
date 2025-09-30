using System.Collections;

namespace Capstone.LMS.Application.Dtos.Dashboard
{
    public record DashboardTableDto(
        string Title,
        IEnumerable Data);
}
