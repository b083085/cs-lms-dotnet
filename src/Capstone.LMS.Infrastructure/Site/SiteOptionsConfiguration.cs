using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Capstone.LMS.Infrastructure.Site
{
    internal sealed class SiteOptionsConfiguration(IConfiguration configuration)
        : IConfigureOptions<SiteOptions>
    {
        public const string ConfigurationSectionName = "Site";
        private readonly IConfiguration _configuration = configuration;

        public void Configure(SiteOptions options)
        {
            _configuration.GetSection(ConfigurationSectionName).Bind(options);
        }
    }
}
