using Capstone.LMS.Application.Dtos.User;

namespace Capstone.LMS.Application.Dtos.Auth
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public GetUserResponseDto User { get; set; }

    }
}
