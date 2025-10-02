using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Capstone.LMS.Infrastructure.Email
{
    internal sealed class SmtpEmailOptionsConfiguration(IConfiguration configuration)
        : IConfigureOptions<SmtpEmailOptions>
    {
        public const string ConfigurationSectionName = "Email:Smtp";
        private readonly IConfiguration _configuration = configuration;

        public void Configure(SmtpEmailOptions options)
        {
            _configuration.GetSection(ConfigurationSectionName).Bind(options);
        }
    }
}
