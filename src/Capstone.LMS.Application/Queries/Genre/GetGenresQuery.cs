using Capstone.LMS.Application.Dtos.Genre;
using MediatR;
using System.Collections.Generic;

namespace Capstone.LMS.Application.Queries.Genre
{
    public record GetGenresQuery() : IRequest<IEnumerable<GetGenreResponseDto>>;
}
