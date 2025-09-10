using Capstone.LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Capstone.LMS.Persistence.Extensions
{
    public static class DbContextExtensions
    {
        public static DbContext EnsureRole(this DbContext dbContext, Guid roleId, string roleName)
        {
            var role = dbContext.Set<Role>().FirstOrDefault(p => p.Name == roleName);
            if (role == null)
            {
                dbContext.Set<Role>().Add(new()
                {
                    Id = roleId,
                    Name = roleName,
                    NormalizedName = roleName.ToUpper()
                });
                dbContext.SaveChanges();
            }

            return dbContext;
        }

        public static async Task<DbContext> EnsureRoleAsync(this DbContext dbContext, Guid roleId, string roleName)
        {
            var role = await dbContext.Set<Role>().FirstOrDefaultAsync(p => p.Name == roleName);
            if (role == null)
            {
                await dbContext.Set<Role>().AddAsync(new()
                {
                    Id = roleId,
                    Name = roleName,
                    NormalizedName = roleName.ToUpper()
                });
                await dbContext.SaveChangesAsync();
            }

            return dbContext;
        }
    }
}
