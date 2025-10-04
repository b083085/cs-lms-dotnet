using Capstone.LMS.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Domain.Repositories
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<Book> GetByIdAsync(Guid bookId, CancellationToken cancellationToken);
    }
}
