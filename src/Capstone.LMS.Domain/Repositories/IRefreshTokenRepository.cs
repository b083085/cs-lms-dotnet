using Capstone.LMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task CreateAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
        Task<RefreshToken> GetAsync(Expression<Func<RefreshToken, bool>> predicate, CancellationToken cancellationToken);
        Task DeleteAllAsync(Expression<Func<RefreshToken, bool>> predicate, CancellationToken cancellationToken);
    }
}
