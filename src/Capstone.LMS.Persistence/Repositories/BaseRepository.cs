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

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await Context.Set<T>().Where(predicate).ToListAsync(cancellationToken);
        }

        public IQueryable<T> GetQueryable()
        {
            return Context.Set<T>().AsQueryable();
        }
    }
}
