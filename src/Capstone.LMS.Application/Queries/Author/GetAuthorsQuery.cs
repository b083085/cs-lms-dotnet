using Capstone.LMS.Application.Dtos.Author;
using Capstone.LMS.Application.Dtos.Genre;
using MediatR;
using System.Collections.Generic;

namespace Capstone.LMS.Application.Queries.Author
{
    public record GetAuthorsQuery() : IRequest<IEnumerable<GetAuthorResponseDto>>;
}
