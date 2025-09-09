using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Capstone.LMS.Persistence.Options
{
    public sealed class DatabaseOptionsConfiguration(IConfiguration configuration)
        : IConfigureOptions<DatabaseOptions>
    {
        public const string ConfigurationSectionName = "Database";

        private readonly IConfiguration _configuration = configuration;

        public void Configure(DatabaseOptions options)
        {
            _configuration.GetSection(ConfigurationSectionName).Bind(options);
        }
    }
}
