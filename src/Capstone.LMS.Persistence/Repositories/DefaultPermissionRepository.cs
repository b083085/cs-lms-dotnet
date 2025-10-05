using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Repositories;

namespace Capstone.LMS.Persistence.Repositories
{
    internal class DefaultPermissionRepository(LmsContext context) : BaseRepository<DefaultPermission>(context), IDefaultPermissionRepository
    {
    }
}
