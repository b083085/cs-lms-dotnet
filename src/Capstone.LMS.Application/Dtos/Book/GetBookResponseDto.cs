using Capstone.LMS.Application.Dtos.Author;
using Capstone.LMS.Application.Dtos.Genre;
using System;

namespace Capstone.LMS.Application.Dtos.Book
{
    public class GetBookResponseDto
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Isbn { get; set; }
        public DateTime PublishedOn { get; set; }
        public int TotalCopies { get; set; }
        public GetGenreResponseDto Genre { get; set; }
        public GetAuthorResponseDto Author { get; set; }
    }
}
