using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Repositories;
using Capstone.LMS.Persistence.Options;
using Capstone.LMS.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Capstone.LMS.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
        {
            AddDbContext(services, configuration);
            AddRepositories(services);

            return services;
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IAccessControlRepository, AccessControlRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBorrowedBookRepository, BorrowedBookRepository>();
            services.AddScoped<IDefaultPermissionRepository, DefaultPermissionRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<ISubPermissionRepository, SubPermissionRepository>();
        }

        private static void AddDbContext(IServiceCollection services, ConfigurationManager configuration)
        {
            var dbOptions = configuration
                            .GetSection(DatabaseOptionsConfiguration.ConfigurationSectionName)
                            .Get<DatabaseOptions>();

            Action<DbContextOptionsBuilder> dbContextOptions = options => options
                .UseSqlServer(dbOptions.ConnectionString, opt => opt.CommandTimeout(dbOptions.CommandTimeout));

            services.AddDbContext<LmsContext>(dbContextOptions, ServiceLifetime.Scoped);
            services.AddDbContextFactory<LmsContext>(dbContextOptions, ServiceLifetime.Scoped);

            services.AddIdentity<User, Role>(opt =>
            {
                opt.SignIn.RequireConfirmedAccount = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 8;
                opt.Password.RequiredUniqueChars = 0;

                // lockout settings
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);
                opt.Lockout.AllowedForNewUsers = false;

                // user settings
                opt.User.RequireUniqueEmail = true;

                // signin service
                opt.SignIn.RequireConfirmedEmail = false;
            })
                .AddEntityFrameworkStores<LmsContext>()
                .AddDefaultTokenProviders();
        }
    }
}
