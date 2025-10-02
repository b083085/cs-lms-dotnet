using Capstone.LMS.Application.Authentication;
using Capstone.LMS.Application.Services;
using Capstone.LMS.Infrastructure.Authentication;
using Capstone.LMS.Infrastructure.BackgroundJobs;
using Capstone.LMS.Infrastructure.Cors;
using Capstone.LMS.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using System.Text;

namespace Capstone.LMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            AddOptions(services);
            AddSecurity(services, configuration);
            AddServices(services);
            AddScheduler(services);

            return services;
        }

        private static void AddSecurity(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddScoped<ITokenProvider, TokenProvider>();

            var jwtOptions = configuration.GetSection(JwtOptionsConfiguration.ConfigurationSectionName).Get<JwtOptions>();
            var corsOptions = configuration.GetSection(CorsOptionsConfiguration.ConfigurationSectionName).Get<Cors.CorsOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters.ValidIssuer = jwtOptions.Issuer;
                options.TokenValidationParameters.ValidAudience = jwtOptions.Audience;
                options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtOptions.SecretKey));
            });

            services.AddAuthorization();

            services.AddHttpContextAccessor();

            services.AddCors(options =>
            options.AddPolicy(Cors.CorsPolicy.AllowOrigin,
                                policy => policy
                                .WithOrigins(corsOptions.Origins?.ToArray())
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials()));
        }

        private static void AddOptions(IServiceCollection services)
        {
            services.ConfigureOptions<JwtOptionsConfiguration>();
            services.ConfigureOptions<CorsOptionsConfiguration>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
        }

        private static void AddScheduler(IServiceCollection services)
        {
            services.AddQuartz(config =>
            {
                var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

                config.AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(trigger =>
                    trigger.ForJob(jobKey)
                    .WithSimpleSchedule(
                        schedule =>
                            schedule.WithIntervalInSeconds(10)
                            .RepeatForever()));
            });

            services.AddQuartzHostedService();
        }
    }
}
