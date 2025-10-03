using Capstone.LMS.Application.Dtos.Genre;
using Capstone.LMS.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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
            return await _genreRepository.GetAllAsync(
                predicate: null,
                sort: g => g.Name,
                noTracking: true,
                transform: g => new GetGenreResponseDto
                {
                    GenreId = g.Id,
                    Name = g.Name
                },
                cancellationToken: cancellationToken);
        }
    }
}
