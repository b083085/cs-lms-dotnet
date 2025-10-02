using Capstone.LMS.Domain.Collections;
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

        public static DbContext EnsureGenres(this DbContext dbContext)
        {
            var genres = new GenreCollection();
            foreach (var genreName in genres)
            {
                var genre = dbContext.Set<Genre>().FirstOrDefault(p => p.Name == genreName);
                if (genre == null)
                {
                    dbContext.Set<Genre>().Add(CreateGenre(genreName));
                    dbContext.SaveChanges();
                }
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

        public static async Task<DbContext> EnsureGenresAsync(this DbContext dbContext)
        {
            var genres = new GenreCollection();
            foreach (var genreName in genres)
            {
                var genre = await dbContext.Set<Genre>().FirstOrDefaultAsync(p => p.Name == genreName);
                if (genre == null)
                {
                    await dbContext.Set<Genre>().AddAsync(CreateGenre(genreName));
                    await dbContext.SaveChangesAsync();
                }
            }

            return dbContext;
        }

        private static Role CreateRole(Guid roleId, string roleName)
        {
            var role = new Role
            {
                Id = roleId,
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            role.Created(Guid.Empty);

            return role;
        }

        private static Genre CreateGenre(string genreName)
        {
            var genre = Genre.Create(
                Guid.NewGuid(),
                genreName);

            genre.Created(Guid.Empty);

            return genre;
        }
    }
}
