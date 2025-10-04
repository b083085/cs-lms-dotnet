using Capstone.LMS.Application.Dtos.Author;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System;

namespace Capstone.LMS.Application.Queries.Author
{
    public record GetAuthorQuery(
        Guid AuthorId) : IRequest<Result<GetAuthorResponseDto>>;
}
