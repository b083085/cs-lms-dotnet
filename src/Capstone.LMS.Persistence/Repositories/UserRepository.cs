using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Repositories;

namespace Capstone.LMS.Persistence.Repositories
{
    internal class UserRepository(LmsContext context) : BaseRepository<User>(context), IUserRepository
    {
    }
}
