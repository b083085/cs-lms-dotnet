using Capstone.LMS.Application.Persistence;

namespace Capstone.LMS.Persistence
{
    internal sealed class UnitOfWork(LmsContext context) : IUnitOfWork
    {
        private readonly LmsContext _context = context;

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
