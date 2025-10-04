using Capstone.LMS.Application.Dtos.User;

namespace Capstone.LMS.Application.Queries.User
{
    public record GetUsersQuery() : BaseListQuery<GetUserResponseDto>();
}
