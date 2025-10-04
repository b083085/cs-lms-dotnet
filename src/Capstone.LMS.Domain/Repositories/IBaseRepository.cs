using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Domain.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> sort,
            bool noTracking = false,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<U>> GetAllAsync<U>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> sort,
            Func<T, U> transform = null,
            bool noTracking = false,
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default);

        Task CreateAsync(
            T entity, 
            CancellationToken cancellationToken);

        Task<T> GetAsync(
            Expression<Func<T, bool>> predicate, 
            CancellationToken cancellationToken);

    }
}
