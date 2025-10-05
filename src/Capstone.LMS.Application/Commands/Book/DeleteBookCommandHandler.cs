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

namespace Capstone.LMS.Application.Commands.Book
{
    public sealed class DeleteBookCommandHandler(
        IBookRepository bookRepository,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        ILogger<DeleteBookCommandHandler> logger)
        : IRequestHandler<DeleteBookCommand, Result>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly ILogger<DeleteBookCommandHandler> _logger = logger;

        public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetAsync(g => g.Id == request.BookId, cancellationToken);
            if(book is null)
            {
                return Result.Failure(DomainErrors.Book.NotFound);
            }

            book.Deleted(_httpContextAccessor.GetCurrentUserId());

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Book is deleted. {BookTitle}", book.Title);

            return Result.Success();
        }
    }
}
