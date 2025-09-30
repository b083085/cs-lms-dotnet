using Capstone.LMS.Application.Dtos.User;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System;

namespace Capstone.LMS.Application.Queries.User
{
    public record GetUserQuery(
        Guid UserId) : IRequest<Result<GetUserResponseDto>>;
}
