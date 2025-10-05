using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capstone.LMS.Persistence.Repositories
{
    internal sealed class BorrowedBookRepository(LmsContext context) : BaseRepository<BorrowedBook>(context), IBorrowedBookRepository
    {
        public override Task<BorrowedBook> GetAsync(Expression<Func<BorrowedBook, bool>> predicate, CancellationToken cancellationToken)
        {
            return Context
                .Set<BorrowedBook>()
                .Include(p => p.Book).ThenInclude(p => p.Genre)
                .Include(p => p.Book).ThenInclude(p => p.Author)
                .Include(p => p.User)
                .Include(p => p.Approver)
                .FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}
