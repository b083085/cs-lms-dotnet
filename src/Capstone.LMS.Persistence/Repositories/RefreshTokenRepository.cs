using Capstone.LMS.Application.Persistence;
using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capstone.LMS.Persistence.Repositories
{
    internal sealed class RefreshTokenRepository(ILMSContext context) : IRefreshTokenRepository
    {
        private readonly ILMSContext _context = context;

        public async Task CreateAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        }

        public async Task DeleteAllAsync(Expression<Func<RefreshToken, bool>> predicate, CancellationToken cancellationToken)
        {
            await _context.RefreshTokens
                .Where(predicate)
                .ExecuteDeleteAsync(cancellationToken);
        }

        public async Task<RefreshToken> GetAsync(Expression<Func<RefreshToken, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.RefreshTokens
                .Include(r => r.User)
                .FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}
