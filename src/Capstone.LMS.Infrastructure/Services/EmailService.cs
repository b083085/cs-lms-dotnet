using Capstone.LMS.Application.Email;
using Capstone.LMS.Application.Services;
using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Exceptions;
using Capstone.LMS.Infrastructure.Site;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;

namespace Capstone.LMS.Infrastructure.Services
{
    internal sealed class EmailService(
        UserManager<User> userManager,
        LinkGenerator links,
        IEmailClient emailClient,
        IOptions<SiteOptions> siteOptions,
        ILogger<EmailService> logger) : IEmailService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly LinkGenerator _links = links;
        private readonly IEmailClient _emailClient = emailClient;
        private readonly SiteOptions _siteOptions = siteOptions.Value;
        private readonly ILogger<EmailService> _logger = logger;

        public async Task SendEmailConfirmationLinkAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedConfirmationToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(emailConfirmationToken));
            var confirmationLink = _siteOptions.Api.BaseUrl + $"/auth/verify?userId={userId}&token={encodedConfirmationToken}";

            var bodyMessage = CreateEmailConfirmationBody(user, confirmationLink);

            var emailMessage = new EmailMessage
            {
                To = user.Email,
                Subject = "Confirm your email to get started",
                Body = bodyMessage
            };

            await _emailClient.SendEmailAsync(emailMessage, cancellationToken);

            _logger.LogInformation("Email confirmation sent successfully for user {UserId}.", userId);
        }


        #region Templates

        private string CreateEmailConfirmationBody(User user, string confirmationLink)
        {
            var sb = new StringBuilder(@"
                <!doctype html>
                <html lang=""en"">
                <head>
                  <meta charset=""utf-8"">
                  <title>Confirm your email</title>
                  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0""/>
                  <style>
                    /* Some clients strip <style> — keep critical styles inline where possible.
                       This small block is safe for responsive tweaks. */
                    @media only screen and (max-width: 600px) {
                      .container { width: 100% !important; }
                      .stack { display:block !important; width:100% !important; }
                      .hero { font-size: 22px !important; }
                    }
                  </style>
                </head>
                <body style=""margin:0;padding:0;background-color:#f4f6f8;font-family:Helvetica, Arial, sans-serif;color:#333333;"">

                  <!-- Preheader: shown in inbox preview (keep short). Hidden visually in the email body. -->
                  <div style=""display:none; max-height:0; overflow:hidden; color:transparent; line-height:0; opacity:0;"">
                    Confirm your email to activate your account.
                  </div>

                  <!-- Main wrapper -->
                  <table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""background-color:#f4f6f8;padding:20px 0;"">
                    <tr>
                      <td align=""center"">

                        <!-- Container -->
                        <table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""600"" class=""container"" style=""max-width:600px;width:100%;background:#ffffff;border-radius:8px;overflow:hidden;box-shadow:0 2px 6px rgba(0,0,0,0.08);"">
                          <!-- Header / Logo area -->
                          <tr>
                            <td style=""padding:24px 28px 0; text-align:left;"">
                              <!-- Replace with your logo image if you have one -->
                              <img src=""https://lle.rcschools.net/apps/download/AeFtnzGXBICaQQTk4ITWhJ2b3JqRY08Wzbs0hVFbpxgytj1z.png/Capstone_Interactive.png"" alt=""Company Logo"" width=""140"" style=""display:block;border:0;outline:none;text-decoration:none;"">
                            </td>
                          </tr>

                          <!-- Hero -->
                          <tr>
                            <td style=""padding:28px;"">
                              <h1 class=""hero"" style=""margin:0 0 12px 0;font-size:28px;line-height:1.2;color:#1f2937;font-weight:600;"">
                                Confirm your email
                              </h1>
                              <p style=""margin:0 0 18px 0;font-size:15px;color:#555;"">
                                Hi <strong>{{firstName}} {{lastName}}</strong>, thanks for signing up. Please confirm your email address (<em>{{email}}</em>) by clicking the button below.
                              </p>

                              <!-- CTA button -->
                              <table role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""margin:22px 0;"">
                                <tr>
                                  <td align=""center"">
                                    <a href=""{{confirmation_link}}""
                                       target=""_blank""
                                       style=""
                                         display:inline-block;
                                         padding:12px 22px;
                                         font-size:16px;
                                         color:#ffffff;
                                         text-decoration:none;
                                         border-radius:6px;
                                         background-color:#2563eb;
                                         border:1px solid #2563eb;
                                         font-weight:600;
                                       ""
                                       role=""button""
                                       aria-label=""Confirm your email"">
                                      Confirm my email
                                    </a>
                                  </td>
                                </tr>
                              </table>

                              <p style=""margin:0 0 8px 0;font-size:13px;color:#6b7280;"">
                                If the button doesn't work, copy and paste this link into your browser:
                              </p>
                              <p style=""word-break:break-all;margin:8px 0 0 0;font-size:13px;color:#1d4ed8;"">
                                <a href=""{{confirmation_link}}"" target=""_blank"" style=""color:#1d4ed8;text-decoration:underline;"">{{confirmation_link}}</a>
                              </p>
                            </td>
                          </tr>

                          <!-- Divider -->
                          <tr>
                            <td style=""padding:0 28px;"">
                              <hr style=""border:none;border-top:1px solid #eef2f7;margin:0;"">
                            </td>
                          </tr>

                          <!-- Account info and help -->
                          <tr>
                            <td style=""padding:18px 28px 28px;"">
                              <p style=""margin:0 0 8px 0;font-size:13px;color:#6b7280;"">
                                Account details:
                              </p>
                              <table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""margin-top:8px;font-size:13px;color:#374151;"">
                                <tr>
                                  <td style=""padding:6px 0;font-weight:600;width:140px;color:#111827;"">First name</td>
                                  <td style=""padding:6px 0;"">{{firstName}}</td>
                                </tr>
                                <tr>
                                  <td style=""padding:6px 0;font-weight:600;color:#111827;"">Last name</td>
                                  <td style=""padding:6px 0;"">{{lastName}}</td>
                                </tr>
                                <tr>
                                  <td style=""padding:6px 0;font-weight:600;color:#111827;"">Email</td>
                                  <td style=""padding:6px 0;"">{{email}}</td>
                                </tr>
                              </table>

                              <p style=""margin:16px 0 0 0;font-size:13px;color:#6b7280;"">
                                Need help? Reply to this email or reach out to our support at
                                <a href=""mailto:support@capstone.com"" style=""color:#1d4ed8;text-decoration:underline;"">support@example.com</a>.
                              </p>
                            </td>
                          </tr>

                          <!-- Footer -->
                          <tr>
                            <td style=""background:#fbfcfe;padding:16px 28px;border-top:1px solid #eef2f7;font-size:12px;color:#9ca3af;"">
                              <p style=""margin:0;"">
                                If you didn't sign up for this account, you can safely ignore this email. This confirmation link will expire in 24 hours.
                              </p>
                            </td>
                          </tr>
                        </table>

                        <!-- Small footer outside main container -->
                        <table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""600"" style=""max-width:600px;width:100%;margin-top:12px;"">
                          <tr>
                            <td align=""center"" style=""font-size:12px;color:#9ca3af;"">
                              <p style=""margin:6px 0 0 0;"">
                                © <span id=""year"">2025</span> Capstone • 1710 Roe Crest Drive North Mankato, MN 56003
                              </p>
                            </td>
                          </tr>
                        </table>

                      </td>
                    </tr>
                  </table>

                </body>
                </html>
");

            sb.Replace("{{firstName}}", user.FirstName);
            sb.Replace("{{lastName}}", user.LastName);
            sb.Replace("{{email}}", user.Email);
            sb.Replace("{{confirmation_link}}", confirmationLink);

            return sb.ToString();
        }

        #endregion

    }
}
