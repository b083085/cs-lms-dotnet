using Capstone.LMS.Application.Dtos.Genre;
using Capstone.LMS.Domain.Errors;
using Capstone.LMS.Domain.Repositories;
using Capstone.LMS.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.Genre
{
    public sealed class GetGenreQueryHandler(
        IGenreRepository genreRepository,
        ILogger<GetGenreQueryHandler> logger) : IRequestHandler<GetGenreQuery, Result<GetGenreResponseDto>>
    {
        private readonly IGenreRepository _genreRepository = genreRepository;
        private readonly ILogger<GetGenreQueryHandler> _logger = logger;

        public async Task<Result<GetGenreResponseDto>> Handle(GetGenreQuery request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetAsync(g => g.Id == request.GenreId, cancellationToken);
            if (genre == null)
            {
                return Result.Failure<GetGenreResponseDto>(DomainErrors.Genre.NotFound);
            }

            return new GetGenreResponseDto
            {
                GenreId = genre.Id,
                Name = genre.Name
            };
        }
    }
}
