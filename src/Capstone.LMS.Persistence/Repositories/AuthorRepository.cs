using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Repositories;

namespace Capstone.LMS.Persistence.Repositories
{
    internal sealed class AuthorRepository(LmsContext context) : BaseRepository<Author>(context), IAuthorRepository
    {
    }
}
