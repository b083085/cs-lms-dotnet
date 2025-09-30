using Capstone.LMS.Application.Dtos.User;

namespace Capstone.LMS.Application.Queries.User
{
    public record GetUsersQuery(
        string SearchTerm,
        SortQuery? Sort,
        PaginationQuery? Pagination) :
        ListQuery<GetUserResponseDto>(SearchTerm, Sort, Pagination);
}
