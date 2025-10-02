using Capstone.LMS.Application.Dtos.Genre;

namespace Capstone.LMS.Application.Queries.Genre
{
    public record GetGenresQuery(
        string SearchTerm,
        SortQuery Sort,
        PaginationQuery Pagination) : 
        ListQuery<GetGenreResponseDto>(SearchTerm, Sort, Pagination); 
}
