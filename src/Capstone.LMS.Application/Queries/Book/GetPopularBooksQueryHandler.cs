using Capstone.LMS.Application.Dtos.Book;
using Capstone.LMS.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.Book
{
    public sealed class GetPopularBooksQueryHandler(
        IBorrowedBookRepository borrowedBookRepository,
        ILogger<GetPopularBooksQueryHandler> logger) 
        : IRequestHandler<GetPopularBooksQuery, IEnumerable<GetPopularBookResponseDto>>
    {
        private readonly IBorrowedBookRepository _borrowedBookRepository = borrowedBookRepository;
        private readonly ILogger<GetPopularBooksQueryHandler> _logger = logger;

        public async Task<IEnumerable<GetPopularBookResponseDto>> Handle(GetPopularBooksQuery request, CancellationToken cancellationToken)
        {
            var query = _borrowedBookRepository.GetQueryable();

            query = query
                .Include(p => p.Book).ThenInclude(p => p.Genre)
                .Include(p => p.Book).ThenInclude(p => p.Author);

            var overdues = await query
                .AsNoTracking()
                .GroupBy(p => new { p.BookId, p.Book.Title, p.Book.Summary })
                .Select(p => new GetPopularBookResponseDto
                {
                    BookId = p.Key.BookId,
                    Title = p.Key.Title,
                    Summary = p.Key.Summary,
                    Total = p.Count()
                })
                .OrderByDescending(p => p.Total)
                .ToListAsync(cancellationToken);

            return overdues;
        }
    }
}
