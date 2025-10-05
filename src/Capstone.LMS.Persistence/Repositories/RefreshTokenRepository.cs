using Capstone.LMS.Application.Persistence;
using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Capstone.LMS.Persistence.Repositories
{
    internal sealed class RefreshTokenRepository(LmsContext context) : BaseRepository<RefreshToken>(context), IRefreshTokenRepository
    {
        private readonly ILMSContext _context = context;

        public async Task DeleteAsync(Expression<Func<RefreshToken, bool>> predicate, CancellationToken cancellationToken)
        {
            await _context.RefreshTokens
                .Where(predicate)
                .ExecuteDeleteAsync(cancellationToken);
        }

        public override async Task<RefreshToken> GetAsync(Expression<Func<RefreshToken, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.RefreshTokens
                .Include(r => r.User)
                .FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}
