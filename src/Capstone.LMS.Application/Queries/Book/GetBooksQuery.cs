using Capstone.LMS.Application.Dtos.Book;

namespace Capstone.LMS.Application.Queries.Book
{
    public record GetBooksQuery(
        string SearchTerm,
        SortQuery? Sort,
        PaginationQuery? Pagination) : 
        ListQuery<GetBookResponseDto>(SearchTerm, Sort, Pagination); 
}
