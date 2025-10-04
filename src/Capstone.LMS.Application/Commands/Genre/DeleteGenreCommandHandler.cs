using Capstone.LMS.Application.Extensions;
using Capstone.LMS.Application.Persistence;
using Capstone.LMS.Domain.Errors;
using Capstone.LMS.Domain.Repositories;
using Capstone.LMS.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Genre
{
    public sealed class DeleteGenreCommandHandler(
        IGenreRepository genreRepository,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        ILogger<DeleteGenreCommandHandler> logger) : IRequestHandler<DeleteGenreCommand, Result>
    {
        private readonly IGenreRepository _genreRepository = genreRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly ILogger<DeleteGenreCommandHandler> _logger = logger;

        public async Task<Result> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetAsync(g => g.Id == request.GenreId, cancellationToken);
            if (genre == null)
            {
                return Result.Failure(DomainErrors.Genre.GenreNotFound);
            }

            genre.Deleted(_httpContextAccessor.GetCurrentUserId());

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Genre is deleted. {Genre}", genre.Name);

            return Result.Success();
        }
    }
}
