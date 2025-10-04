using Capstone.LMS.Application.Persistence;
using Capstone.LMS.Domain.Errors;
using Capstone.LMS.Domain.Repositories;
using Capstone.LMS.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Author
{
    public sealed class UpdateAuthorCommandHandler(
        IAuthorRepository authorRepository,
        IUnitOfWork unitOfWork,
        ILogger<UpdateAuthorCommandHandler> logger
        ) : IRequestHandler<UpdateAuthorCommand, Result>
    {
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<UpdateAuthorCommandHandler> _logger = logger;

        public async Task<Result> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetAsync(g => g.Id == request.AuthorId, cancellationToken);
            if (author == null)
            {
                return Result.Failure(DomainErrors.Author.AuthorNotFound);
            }

            author.SetName(request.Name);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Author is updated. {Author}", author.Name);

            return Result.Success();
        }
    }
}
