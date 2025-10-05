using System;

namespace Capstone.LMS.Application.Dtos.Book
{
    public class GetPopularBookResponseDto
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public int Total { get; set; }
    }
}
