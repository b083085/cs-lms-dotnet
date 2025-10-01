using Capstone.LMS.Application.Dtos.Book;

namespace Capstone.LMS.Application.Queries.Book
{
    public record GetBorrowedBooksQuery(
        string SearchTerm,
        SortQuery Sort,
        PaginationQuery Pagination) :
        ListQuery<GetBorrowedBookResponseDto>(SearchTerm, Sort, Pagination);
}
