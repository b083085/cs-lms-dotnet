using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Repositories;

namespace Capstone.LMS.Persistence.Repositories
{
    internal class AccessControlRepository(LmsContext context) : BaseRepository<AccessControl>(context), IAccessControlRepository
    {
    }
}
