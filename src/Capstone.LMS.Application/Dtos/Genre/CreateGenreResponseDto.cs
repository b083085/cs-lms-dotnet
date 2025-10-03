using System;

namespace Capstone.LMS.Application.Dtos.Genre
{
    public class CreateGenreResponseDto
    {
        public Guid GenreId { get; set; }
        public string Name { get; set; }
    }
}
