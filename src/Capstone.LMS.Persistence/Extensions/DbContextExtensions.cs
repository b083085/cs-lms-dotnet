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
                dbContext.Set<Role>().Add(CreateRole(roleId, roleName));
                dbContext.SaveChanges();
            }

            return dbContext;
        }

        public static async Task<DbContext> EnsureRoleAsync(this DbContext dbContext, Guid roleId, string roleName)
        {
            var role = await dbContext.Set<Role>().FirstOrDefaultAsync(p => p.Name == roleName);
            if (role == null)
            {
                await dbContext.Set<Role>().AddAsync(CreateRole(roleId, roleName));
                await dbContext.SaveChangesAsync();
            }

            return dbContext;
        }

        private static Role CreateRole(Guid roleId, string roleName)
        {
            return new()
            {
                Id = roleId,
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
        }
    }
}
