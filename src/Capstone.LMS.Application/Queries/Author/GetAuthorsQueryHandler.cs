using Capstone.LMS.Application.Dtos.Author;
using Capstone.LMS.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.Author
{
    public sealed class GetAuthorsQueryHandler(
        IAuthorRepository authorRepository,
        ILogger<GetAuthorsQueryHandler> logger) : 
        IRequestHandler<GetAuthorsQuery, IEnumerable<GetAuthorResponseDto>>
    {
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly ILogger<GetAuthorsQueryHandler> _logger = logger;

        public async Task<IEnumerable<GetAuthorResponseDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            return await _authorRepository.GetAllAsync(
                predicate: null,
                sort: a => a.Name,
                noTracking: true,
                transform: a => new GetAuthorResponseDto
                {
                    AuthorId = a.Id,
                    Name = a.Name
                },
                cancellationToken: cancellationToken);
        }
    }
}
