using Capstone.LMS.Application.Dtos.Genre;
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
    public sealed class CreateGenreCommandHandler(
        IGenreRepository genreRepository,
        IUnitOfWork unitOfWork,
        ILogger<CreateGenreCommandHandler> logger) : IRequestHandler<CreateGenreCommand, Result<CreateGenreResponseDto>>
    {
        private readonly IGenreRepository _genreRepository = genreRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<CreateGenreCommandHandler> _logger = logger;

        public async Task<Result<CreateGenreResponseDto>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetAsync(g => g.Name.ToLower() == request.Name.ToLower(), cancellationToken);
            if(genre is not null)
            {
                return Result.Failure<CreateGenreResponseDto>(DomainErrors.Genre.GenreAlreadyExist);
            }

            genre = Domain.Entities.Genre.Create(Guid.NewGuid(), request.Name);

            await _genreRepository.CreateAsync(genre, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Genre is created. {Genre}", genre.Name);

            return new CreateGenreResponseDto
            {
                GenreId = genre.Id,
                Name = genre.Name
            };
        }
    }
}
