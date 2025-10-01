using Capstone.LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Capstone.LMS.Application.Persistence
{
    public interface ILMSContext
    {
        DbSet<AccessControl> AccessControls { get; }
        DbSet<Book> Books { get; }
        DbSet<BorrowedBook> BorrowedBooks { get; }
        DbSet<Permission> Permissions { get; }
        DbSet<SubPermission> SubPermissions { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
    }
}
