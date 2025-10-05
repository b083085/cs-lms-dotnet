using Capstone.LMS.Application.Dtos.Book;
using Capstone.LMS.Domain.Enums;

namespace Capstone.LMS.Application.Queries.Book
{
    public record GetBooksQuery() : BaseListQuery<GetBookItemResponseDto>()
    {
        public int? PublishedYear { get; init; } = null;
        public string Author { get; init; } = string.Empty;
        public Availability? Availability { get; init; } = null;
    }
}
