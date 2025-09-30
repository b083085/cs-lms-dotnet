using Capstone.LMS.Application.Behaviors;
using Capstone.LMS.Application.Mapper;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Capstone.LMS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager configuration)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            
            AddMediatR(services, assembly);
            AddMapster(services);

            return services;
        }

        private static void AddMediatR(IServiceCollection services, System.Reflection.Assembly assembly)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
                config.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.AddValidatorsFromAssembly(assembly);
        }

        private static void AddMapster(IServiceCollection services)
        {
            var mapperConfig = TypeAdapterConfig.GlobalSettings.ConfigureMappings();

            services.AddSingleton(mapperConfig);
            services.AddMapster();
        }
    }
}
