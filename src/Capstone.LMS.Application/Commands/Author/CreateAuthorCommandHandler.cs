using Capstone.LMS.Application.Dtos.Author;
using Capstone.LMS.Application.Persistence;
using Capstone.LMS.Domain.Errors;
using Capstone.LMS.Domain.Repositories;
using Capstone.LMS.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Author
{
    public sealed class CreateAuthorCommandHandler(
        IAuthorRepository authorRepository,
        IUnitOfWork unitOfWork,
        ILogger<CreateAuthorCommandHandler> logger) : IRequestHandler<CreateAuthorCommand, Result<CreateAuthorResponseDto>>
    {
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<CreateAuthorCommandHandler> _logger = logger;

        public async Task<Result<CreateAuthorResponseDto>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetAsync(g => g.Name.ToLower() == request.Name.ToLower(), cancellationToken);
            if(author is not null)
            {
                return Result.Failure<CreateAuthorResponseDto>(DomainErrors.Author.AlreadyExist);
            }

            author = Domain.Entities.Author.Create(Guid.NewGuid(), request.Name);

            await _authorRepository.CreateAsync(author, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Author is created. {Author}", request.Name);

            return new CreateAuthorResponseDto
            {
                AuthorId = author.Id,
                Name = author.Name
            };
        }
    }
}
