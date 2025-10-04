using Capstone.LMS.Domain.Primitives;
using Capstone.LMS.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capstone.LMS.Persistence.Repositories
{
    internal class BaseRepository<T>(LmsContext context) : IBaseRepository<T> where T : class
    {
        public LmsContext Context { get; } = context;

        public async Task CreateAsync(T entity, CancellationToken cancellationToken)
        {
            await Context.Set<T>().AddAsync(entity, cancellationToken);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate, 
            Expression<Func<T, object>> sort, 
            bool noTracking = false, 
            int? skip = null, 
            int? take = null, 
            CancellationToken cancellationToken = default)
        {
            return await GetAllAsync(
                predicate,
                sort,
                t => t,
                noTracking,
                skip,
                take,
                cancellationToken);
        }

        public async Task<IEnumerable<U>> GetAllAsync<U>(
            Expression<Func<T, bool>> predicate, 
            Expression<Func<T, object>> sort, 
            Func<T, U> transform = null, 
            bool noTracking = false, 
            int? skip = null, 
            int? take = null, 
            CancellationToken cancellationToken = default)
        {
            var query = Context.Set<T>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            if (sort != null)
            {
                query = query.OrderBy(sort);
            }

            if (skip != null && take != null)
            {
                query = query
                    .Skip(skip.Value)
                    .Take(take.Value);
            }

            return await query
                .OrderBy(sort)
                .Select(g => transform.Invoke(g))
                .ToListAsync(cancellationToken);
        }
    }
}
