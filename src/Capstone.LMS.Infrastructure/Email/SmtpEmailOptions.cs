namespace Capstone.LMS.Infrastructure.Email
{
    public class SmtpEmailOptions
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
