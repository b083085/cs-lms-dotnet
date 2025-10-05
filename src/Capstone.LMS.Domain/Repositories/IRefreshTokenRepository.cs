using Capstone.LMS.Domain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Domain.Repositories
{
    public interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
    {
        Task DeleteAsync(Expression<Func<RefreshToken, bool>> predicate, CancellationToken cancellationToken);
    }
}
