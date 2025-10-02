using Capstone.LMS.Application.Dtos.Genre;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System;

namespace Capstone.LMS.Application.Queries.Genre
{
    public record GetGenreQuery(
        Guid BookId) : IRequest<Result<GetGenreResponseDto>>;
}
