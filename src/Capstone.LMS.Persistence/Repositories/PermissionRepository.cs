using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Repositories;


namespace Capstone.LMS.Persistence.Repositories
{
    internal class PermissionRepository(LmsContext context) : BaseRepository<Permission>(context), IPermissionRepository
    {
    }
}
