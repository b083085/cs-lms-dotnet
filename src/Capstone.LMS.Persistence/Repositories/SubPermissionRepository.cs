using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Repositories;

namespace Capstone.LMS.Persistence.Repositories
{
    internal class SubPermissionRepository(LmsContext context) : BaseRepository<SubPermission>(context), ISubPermissionRepository
    {
    }
}
