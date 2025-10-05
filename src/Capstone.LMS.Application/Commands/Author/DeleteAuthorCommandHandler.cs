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

namespace Capstone.LMS.Application.Commands.Author
{
    public sealed class DeleteAuthorCommandHandler(
        IAuthorRepository authorRepository,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        ILogger<DeleteAuthorCommandHandler> logger) : IRequestHandler<DeleteAuthorCommand, Result>
    {
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly ILogger<DeleteAuthorCommandHandler> _logger = logger;

        public async Task<Result> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetAsync(g => g.Id == request.AuthorId, cancellationToken);
            if (author == null)
            {
                return Result.Failure(DomainErrors.Author.NotFound);
            }

            author.Deleted(_httpContextAccessor.GetCurrentUserId());

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Author is deleted. {Author}", author.Name);

            return Result.Success();
        }
    }
}
