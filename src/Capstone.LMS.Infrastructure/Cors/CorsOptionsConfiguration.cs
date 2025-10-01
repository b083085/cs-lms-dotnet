using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Capstone.LMS.Infrastructure.Cors
{
    internal sealed class CorsOptionsConfiguration(IConfiguration configuration) : IConfigureOptions<CorsOptions>
    {
        public const string ConfigurationSectionName = "Cors";
        private readonly IConfiguration _configuration = configuration;

        public void Configure(CorsOptions options)
        {
            _configuration.GetSection(ConfigurationSectionName).Bind(options);
        }
    }
}
