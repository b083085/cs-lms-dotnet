using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Repositories;

namespace Capstone.LMS.Persistence.Repositories
{
    internal sealed class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(LmsContext context)
            : base(context)
        {
        }
    }
}
