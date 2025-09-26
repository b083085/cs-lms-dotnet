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

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
                config.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.AddValidatorsFromAssembly(assembly);
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            var mapperConfig = TypeAdapterConfig.GlobalSettings.ConfigureMappings();

            services.AddSingleton(mapperConfig);
            services.AddMapster();

            return services;
        }
    }
}
