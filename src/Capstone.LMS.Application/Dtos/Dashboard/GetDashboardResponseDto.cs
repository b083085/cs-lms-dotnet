using System.Collections.Generic;

namespace Capstone.LMS.Application.Dtos.Dashboard
{
    public class GetDashboardResponseDto
    {
        public IEnumerable<DashboardCardDto> Cards { get; set; }
        public IEnumerable<DashboardChartDto> Charts { get; set; }
        public IEnumerable<DashboardTableDto> Tables { get; set; }
        public IEnumerable<DashboardListDto> List {  get; set; }
    }
}
