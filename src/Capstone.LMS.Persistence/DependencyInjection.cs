using Capstone.LMS.Application.Persistence;
using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Repositories;
using Capstone.LMS.Persistence.Interceptors;
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
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        }

        private static void AddDbContext(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddScoped<AuditSaveChangesInterceptor>();
            services.AddScoped<PublicIdSaveChangesInterceptor>();


            var dbOptions = configuration
                            .GetSection(DatabaseOptionsConfiguration.ConfigurationSectionName)
                            .Get<DatabaseOptions>();

            Action<IServiceProvider, DbContextOptionsBuilder> dbContextOptions = (sp, options) =>
            {
                options.UseSqlServer(dbOptions.ConnectionString, opt => opt.CommandTimeout(dbOptions.CommandTimeout));

                var publicIdInterceptor = sp.GetRequiredService<PublicIdSaveChangesInterceptor>();
                var auditInterceptor = sp.GetRequiredService<AuditSaveChangesInterceptor>();

                options.AddInterceptors(
                    publicIdInterceptor,
                    auditInterceptor);
            };

            services.AddDbContext<LmsContext>(dbContextOptions, ServiceLifetime.Scoped);
            services.AddDbContextFactory<LmsContext>(dbContextOptions, ServiceLifetime.Scoped);

            services.AddIdentity<User, Role>(opt =>
            {
                opt.SignIn.RequireConfirmedAccount = true;
                opt.Password.RequireDigit = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 8;
                opt.Password.RequiredUniqueChars = 0;

                // lockout settings
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);
                opt.Lockout.AllowedForNewUsers = false;

                // user settings
                opt.User.RequireUniqueEmail = true;

                // signin service
                opt.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<LmsContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<ILMSContext>(sp => sp.GetRequiredService<LmsContext>());
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
