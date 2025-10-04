using System;

namespace Capstone.LMS.Application.Dtos.Author
{
    public class GetAuthorResponseDto
    {
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
    }
}
