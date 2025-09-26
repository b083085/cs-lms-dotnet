using Capstone.LMS.Application.Persistence;
using Capstone.LMS.Domain.Constants;
using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Persistence.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Capstone.LMS.Persistence
{
    public sealed class LmsContext(DbContextOptions<LmsContext> options)
        : IdentityDbContext<User, Role, Guid>(options), ILMSContext
    {
        public DbSet<AccessControl> AccessControls { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<SubPermission> SubPermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(LmsContext).Assembly);

            #region Identity Customization

            builder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });
            builder.Entity<IdentityUserRole<Guid>>().ToTable(EntityTables.UserRoles);

            builder.Entity<IdentityRoleClaim<Guid>>().HasKey(p => p.Id);
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable(EntityTables.RoleClaims);

            builder.Entity<IdentityUserClaim<Guid>>().HasKey(p => p.Id);
            builder.Entity<IdentityUserClaim<Guid>>().ToTable(EntityTables.UserClaims);

            builder.Entity<IdentityUserLogin<Guid>>().HasKey(p => p.UserId);
            builder.Entity<IdentityUserLogin<Guid>>().ToTable(EntityTables.UserLogins);

            builder.Entity<IdentityUserToken<Guid>>().HasKey(p => p.UserId);
            builder.Entity<IdentityUserToken<Guid>>().ToTable(EntityTables.UserTokens);

            #endregion
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSeeding((ctx, _) =>
            {
                ctx
                .EnsureRole(Domain.Constants.Roles.AdministratorId, Domain.Constants.Roles.Administrator)
                .EnsureRole(Domain.Constants.Roles.LibrarianId, Domain.Constants.Roles.Librarian)
                .EnsureRole(Domain.Constants.Roles.BorrowerId, Domain.Constants.Roles.Borrower);

            }).UseAsyncSeeding(async (ctx, _, cancellationToken) =>
            {
                await ctx.EnsureRoleAsync(Domain.Constants.Roles.AdministratorId, Domain.Constants.Roles.Administrator);
                await ctx.EnsureRoleAsync(Domain.Constants.Roles.LibrarianId, Domain.Constants.Roles.Librarian);
                await ctx.EnsureRoleAsync(Domain.Constants.Roles.BorrowerId, Domain.Constants.Roles.Borrower);    
            });
        }
    }
}
