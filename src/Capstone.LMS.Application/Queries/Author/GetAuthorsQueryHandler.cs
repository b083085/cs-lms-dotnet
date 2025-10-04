using Capstone.LMS.Application.Dtos.Author;
using Capstone.LMS.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
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
            var query = _authorRepository.GetQueryable();

            var authors = await query
                .Select(p => new GetAuthorResponseDto
                {
                    AuthorId = p.Id,
                    Name = p.Name
                })
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

            return authors;
        }
    }
}
