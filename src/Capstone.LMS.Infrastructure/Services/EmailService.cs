using Capstone.LMS.Application.Email;
using Capstone.LMS.Application.Services;
using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Exceptions;
using Capstone.LMS.Domain.Repositories;
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
        IBorrowedBookRepository borrowedBookRepository,
        LinkGenerator links,
        IEmailClient emailClient,
        IOptions<SiteOptions> siteOptions,
        ILogger<EmailService> logger) : IEmailService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IBorrowedBookRepository _borrowedBookRepository = borrowedBookRepository;
        private readonly LinkGenerator _links = links;
        private readonly IEmailClient _emailClient = emailClient;
        private readonly SiteOptions _siteOptions = siteOptions.Value;
        private readonly ILogger<EmailService> _logger = logger;

        public async Task SendEmailConfirmationLinkAsync(Guid userId, CancellationToken cancellationToken)
        {
            User user = await GetUserAsync(userId);

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

        public async Task SendRequestToBorrowBookApprovedEmailAsync(Guid bookBorrowedId, CancellationToken cancellationToken)
        {
            await SendRequestToBorrowBookStatusEmailAsync(bookBorrowedId, CreateRequestToBorrowBookApprovedBody, "Approved", cancellationToken);
        }

        public async Task SendRequestToBorrowBookRejectedEmailAsync(Guid bookBorrowedId, CancellationToken cancellationToken)
        {
            await SendRequestToBorrowBookStatusEmailAsync(bookBorrowedId, CreateRequestToBorrowBookRejectedBody, "Rejected", cancellationToken);
        }

        private async Task SendRequestToBorrowBookStatusEmailAsync(Guid bookBorrowedId, Func<BorrowedBook, string> bodyFunc, string status, CancellationToken cancellationToken)
        {
            var bookBorrowed = await _borrowedBookRepository.GetAsync(b => b.Id == bookBorrowedId, cancellationToken) ?? throw new BookBorrowedNotFoundException(bookBorrowedId);

            var user = await GetUserAsync(bookBorrowed.UserId);

            var bodyMessage = bodyFunc(bookBorrowed);

            var emailMessage = new EmailMessage
            {
                To = user.Email,
                Subject = $"Book Borrow Request {status}",
                Body = bodyMessage
            };

            await _emailClient.SendEmailAsync(emailMessage, cancellationToken);

            _logger.LogInformation("Request to borrow book {Status} email sent successfully for user {Email}.", status, user.Email);
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
                                <a href=""mailto:support@library.com"" style=""color:#1d4ed8;text-decoration:underline;"">support@library.com</a>.
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

        private string CreateRequestToBorrowBookApprovedBody(BorrowedBook borrowedBook)
        {
            var sb = new StringBuilder(@"
<!-- Replace placeholders like {{borrowerName}} with real values -->
<!doctype html>
<html>
  <head>
    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width,initial-scale=1"" />
    <title>Borrow Request Approved</title>
  </head>
  <body style=""margin:0;padding:0;background-color:#f4f4f6;font-family:Arial, sans-serif;"">
    <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background-color:#f4f4f6;padding:20px 0;"">
      <tr>
        <td align=""center"">
          <table role=""presentation"" width=""600"" cellpadding=""0"" cellspacing=""0"" style=""background:#ffffff;border-radius:8px;overflow:hidden;box-shadow:0 2px 6px rgba(0,0,0,0.08);"">
            <!-- Header -->
            <tr>
              <td style=""background:#2a7ae2;padding:18px 24px;color:#ffffff;"">
                <h1 style=""margin:0;font-size:18px;font-weight:600;"">Library Notification</h1>
              </td>
            </tr>

            <!-- Body -->
            <tr>
              <td style=""padding:20px 24px;color:#333333;"">
                <p style=""margin:0 0 12px 0;font-size:15px;"">
                  Hello <strong>{{borrowerName}}</strong>,
                </p>

                <p style=""margin:0 0 12px 0;font-size:15px;line-height:1.5;"">
                  Good news — your request to borrow the book
                  <strong>""{{bookTitle}}""</strong> has been <strong>approved</strong>.
                </p>

                <table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""margin:12px 0 18px 0;"">
                  <tr>
                    <td style=""font-size:14px;padding:6px 0;""><strong>Request ID:</strong></td>
                    <td style=""font-size:14px;padding:6px 0;"">{{requestId}}</td>
                  </tr>
                  <tr>
                    <td style=""font-size:14px;padding:6px 0;""><strong>Issued On:</strong></td>
                    <td style=""font-size:14px;padding:6px 0;"">{{issuedOn}}</td>
                  </tr>
                  <tr>
                    <td style=""font-size:14px;padding:6px 0;""><strong>Due Date:</strong></td>
                    <td style=""font-size:14px;padding:6px 0;"">{{dueDate}}</td>
                  </tr>
                </table>

                <p style=""margin:0 0 18px 0;font-size:14px;line-height:1.5;color:#555555;"">
                  Please bring a valid ID when you pick up the book.
                </p>

                <p style=""margin:0;font-size:13px;color:#888888;"">
                  Thank you,<br/>
                  <strong>Library Management</strong>
                </p>
              </td>
            </tr>

            <!-- Footer -->
            <tr>
              <td style=""background:#fafafa;padding:12px 24px;color:#999999;font-size:12px;"">
                <div>
                  If you did not request this book, please contact us immediately at
                  <a href=""mailto:support@library.com"" style=""color:#2a7ae2;text-decoration:none;"">support@library.com</a>.
                </div>
              </td>
            </tr>

          </table>
        </td>
      </tr>
    </table>
  </body>
</html>
");

            sb.Replace("{{borrowerName}}", borrowedBook.User.GetFullName());
            sb.Replace("{{bookTitle}}", borrowedBook.Book.Title);
            sb.Replace("{{requestId}}", borrowedBook.Id.ToString());
            sb.Replace("{{issuedOn}}", borrowedBook.BorrowedOnUtc?.ToString("yyyy-MM-dd HH:mm:ss tt"));
            sb.Replace("{{dueDate}}", borrowedBook.DueOnUtc?.ToString("yyyy-MM-dd HH:mm:ss tt"));

            return sb.ToString();
        }

        private string CreateRequestToBorrowBookRejectedBody(BorrowedBook borrowedBook)
        {
            var sb = new StringBuilder(@"
<!doctype html>
<html>
  <head>
    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width,initial-scale=1"" />
    <title>Borrow Request Rejected</title>
  </head>
  <body style=""margin:0;padding:0;background-color:#f4f4f6;font-family:Arial, sans-serif;"">
    <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background-color:#f4f4f6;padding:20px 0;"">
      <tr>
        <td align=""center"">
          <table role=""presentation"" width=""600"" cellpadding=""0"" cellspacing=""0"" style=""background:#ffffff;border-radius:8px;overflow:hidden;box-shadow:0 2px 6px rgba(0,0,0,0.08);"">
            <!-- Header -->
            <tr>
              <td style=""background:#d93025;padding:18px 24px;color:#ffffff;"">
                <h1 style=""margin:0;font-size:18px;font-weight:600;"">Library Notification</h1>
              </td>
            </tr>

            <!-- Body -->
            <tr>
              <td style=""padding:20px 24px;color:#333333;"">
                <p style=""margin:0 0 12px 0;font-size:15px;"">
                  Hello <strong>{{borrowerName}}</strong>,
                </p>

                <p style=""margin:0 0 12px 0;font-size:15px;line-height:1.5;"">
                  We’re sorry to inform you that your request to borrow the book
                  <strong>""{{bookTitle}}""</strong> has been <strong style=""color:#d93025;"">rejected</strong>.
                </p>

                <table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""margin:12px 0 18px 0;"">
                  <tr>
                    <td style=""font-size:14px;padding:6px 0;""><strong>Request ID:</strong></td>
                    <td style=""font-size:14px;padding:6px 0;"">{{requestId}}</td>
                  </tr>
                  <tr>
                    <td style=""font-size:14px;padding:6px 0;""><strong>Requested On:</strong></td>
                    <td style=""font-size:14px;padding:6px 0;"">{{requestDate}}</td>
                  </tr>
                  <tr>
                    <td style=""font-size:14px;padding:6px 0;""><strong>Reason:</strong></td>
                    <td style=""font-size:14px;padding:6px 0;color:#d93025;"">{{rejectionReason}}</td>
                  </tr>
                </table>

                <p style=""margin:0 0 18px 0;font-size:14px;line-height:1.5;color:#555555;"">
                  You may request another book or contact the library for assistance.
                  If you believe this decision was made in error, please reply to this email or reach out to us at
                  <a href=""mailto:support@library.com"" style=""color:#2a7ae2;text-decoration:none;"">support@library.com</a>.
                </p>

                <p style=""margin:0;font-size:13px;color:#888888;"">
                  Thank you,<br/>
                  <strong>Library Management</strong>
                </p>
              </td>
            </tr>

            <!-- Footer -->
            <tr>
              <td style=""background:#fafafa;padding:12px 24px;color:#999999;font-size:12px;"">
                <div>
                  This is an automated message from Library.
                  If you have questions, please contact
                  <a href=""mailto:support@library.com"" style=""color:#2a7ae2;text-decoration:none;"">support@library.com</a>.
                </div>
              </td>
            </tr>

          </table>
        </td>
      </tr>
    </table>
  </body>
</html>
");

            sb.Replace("{{borrowerName}}", borrowedBook.User.GetFullName());
            sb.Replace("{{bookTitle}}", borrowedBook.Book.Title);
            sb.Replace("{{requestId}}", borrowedBook.Id.ToString());
            sb.Replace("{{requestDate}}", borrowedBook.CreatedOnUtc.ToString("yyyy-MM-dd HH:mm:ss tt"));
            sb.Replace("{{rejectionReason}}", string.Empty);

            return sb.ToString();
        }

        private async Task<User> GetUserAsync(Guid userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);
        }

        #endregion

    }
}
