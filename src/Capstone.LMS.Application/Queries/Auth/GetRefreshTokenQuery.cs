using Capstone.LMS.Application.Dtos.Auth;
using Capstone.LMS.Domain.Shared;
using MediatR;

namespace Capstone.LMS.Application.Queries.Auth
{
    public record GetRefreshTokenQuery(
        string RefreshToken) : IRequest<Result<GetRefreshTokenResponseDto>>;
}
