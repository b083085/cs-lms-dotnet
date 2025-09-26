using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Persistence
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
