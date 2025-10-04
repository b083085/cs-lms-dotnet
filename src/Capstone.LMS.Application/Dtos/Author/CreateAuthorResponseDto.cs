using System;

namespace Capstone.LMS.Application.Dtos.Author
{
    public class CreateAuthorResponseDto
    {
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
    }
}
