using Capstone.LMS.Application.Dtos.Book;
using Capstone.LMS.Domain.Enums;
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
    public sealed class GetOverdueBooksQueryHandler(
        IBorrowedBookRepository borrowedBookRepository,
        ILogger<GetOverdueBooksQueryHandler> logger) 
        : IRequestHandler<GetOverdueBooksQuery, IEnumerable<GetOverdueBookResponseDto>>
    {
        private readonly IBorrowedBookRepository _borrowedBookRepository = borrowedBookRepository;
        private readonly ILogger<GetOverdueBooksQueryHandler> _logger = logger;

        public async Task<IEnumerable<GetOverdueBookResponseDto>> Handle(GetOverdueBooksQuery request, CancellationToken cancellationToken)
        {
            var query = _borrowedBookRepository.GetQueryable();

            query = query
                .Include(p => p.Book).ThenInclude(p => p.Genre)
                .Include(p => p.Book).ThenInclude(p => p.Author);

            var overdues = await query
                .Where(p => p.Status == BorrowedStatus.Overdue)
                .AsNoTracking()
                .GroupBy(p => new { p.BookId, p.Book.Title, p.Book.Summary })
                .Select(p => new GetOverdueBookResponseDto
                {
                    BookId = p.Key.BookId,
                    Title = p.Key.Title,
                    Summary = p.Key.Summary,
                    Total = p.Count()
                })
                .ToListAsync(cancellationToken);

            return overdues;

        }
    }
}
