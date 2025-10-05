using System;

namespace Capstone.LMS.Application.Dtos.Dashboard
{
    public class DashboardBorrowedBookItemDto
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime? Issued { get; set; }
        public DateTime? Due { get; set; }
    }
}
