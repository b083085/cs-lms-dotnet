using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capstone.LMS.Persistence.Repositories
{
    internal sealed class BookRepository(LmsContext context) : BaseRepository<Book>(context), IBookRepository
    {
        public override async Task<Book> GetAsync(Expression<Func<Book, bool>> predicate, CancellationToken cancellationToken)
        {
            return await Context
                .Set<Book>()
                .Include(b => b.Genre)
                .Include(b => b.Author)
                .Include(b => b.BorrowedBooks)
                .Where(predicate)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
