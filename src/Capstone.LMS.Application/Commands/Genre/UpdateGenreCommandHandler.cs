using Capstone.LMS.Application.Persistence;
using Capstone.LMS.Domain.Errors;
using Capstone.LMS.Domain.Repositories;
using Capstone.LMS.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Genre
{
    public sealed class UpdateGenreCommandHandler(
        IGenreRepository genreRepository,
        IUnitOfWork unitOfWork,
        ILogger<UpdateGenreCommandHandler> logger
        ) : IRequestHandler<UpdateGenreCommand, Result>
    {
        private readonly IGenreRepository _genreRepository = genreRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<UpdateGenreCommandHandler> _logger = logger;

        public async Task<Result> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetAsync(g => g.Id == request.GenreId, cancellationToken);
            if (genre == null)
            {
                return Result.Failure(DomainErrors.Genre.GenreNotFound);
            }

            genre.SetName(request.Name);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Genre is updated. {Genre}", genre.Name);

            return Result.Success();
        }
    }
}
