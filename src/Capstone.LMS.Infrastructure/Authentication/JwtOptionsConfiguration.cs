using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Capstone.LMS.Infrastructure.Authentication
{
    internal sealed class JwtOptionsConfiguration(IConfiguration configuration) 
        : IConfigureOptions<JwtOptions>
    {
        public const string ConfigurationSectionName = "Jwt";
        private readonly IConfiguration _configuration = configuration;

        public void Configure(JwtOptions options)
        {
            _configuration.GetSection(ConfigurationSectionName).Bind(options);
        }
    }
}
