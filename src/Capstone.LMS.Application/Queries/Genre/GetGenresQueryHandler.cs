using Capstone.LMS.Application.Dtos.Genre;
using Capstone.LMS.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.Genre
{
    public sealed class GetGenresQueryHandler(
        IGenreRepository genreRepository,
        ILogger<GetGenreQueryHandler> logger) : 
        IRequestHandler<GetGenresQuery, IEnumerable<GetGenreResponseDto>>
    {
        private readonly IGenreRepository _genreRepository = genreRepository;
        private readonly ILogger<GetGenreQueryHandler> _logger = logger;

        public async Task<IEnumerable<GetGenreResponseDto>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
        {
            var query = _genreRepository.GetQueryable();

            var genres = await query
                .Select(p => new GetGenreResponseDto
                {
                    GenreId = p.Id,
                    Name = p.Name
                })
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

            return genres;
        }
    }
}
