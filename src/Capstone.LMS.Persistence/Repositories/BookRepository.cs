using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Capstone.LMS.Persistence.Repositories
{
    internal sealed class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(LmsContext context)
            : base(context)
        {
        }

        public async Task<Book> GetByIdAsync(Guid bookId, CancellationToken cancellationToken)
        {
            return await Context
               .Set<Book>()
               .Include(b => b.Genre)
               .Include(b => b.Author)
               .FirstOrDefaultAsync(b => b.Id == bookId, cancellationToken);
        }
    }
}
