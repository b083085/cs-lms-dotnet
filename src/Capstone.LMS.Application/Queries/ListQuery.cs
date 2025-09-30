using Capstone.LMS.Application.Dtos;
using MediatR;

namespace Capstone.LMS.Application.Queries
{
    public record ListQuery<T>(
        string SearchTerm,
        SortQuery? Sort,
        PaginationQuery? Pagination) : 
        IRequest<ListResponseDto<T>>;
}
