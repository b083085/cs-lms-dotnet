using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Domain.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        IQueryable<T> GetQueryable();

        Task CreateAsync(T entity, CancellationToken cancellationToken);
    }
}
