using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Persistence.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.LMS.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
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

            return services;
        }
    }
}
