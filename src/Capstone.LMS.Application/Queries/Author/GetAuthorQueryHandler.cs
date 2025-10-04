using Capstone.LMS.Application.Dtos.Author;
using Capstone.LMS.Domain.Errors;
using Capstone.LMS.Domain.Repositories;
using Capstone.LMS.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.Author
{
    public sealed class GetAuthorQueryHandler(
        IAuthorRepository authorRepository,
        ILogger<GetAuthorQueryHandler> logger) : IRequestHandler<GetAuthorQuery, Result<GetAuthorResponseDto>>
    {
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly ILogger<GetAuthorQueryHandler> _logger = logger;

        public async Task<Result<GetAuthorResponseDto>> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetAsync(g => g.Id == request.AuthorId, cancellationToken);
            if (author == null)
            {
                return Result.Failure<GetAuthorResponseDto>(DomainErrors.Author.AuthorNotFound);
            }

            return new GetAuthorResponseDto
            {
                AuthorId = author.Id,
                Name = author.Name
            };
        }
    }
}
