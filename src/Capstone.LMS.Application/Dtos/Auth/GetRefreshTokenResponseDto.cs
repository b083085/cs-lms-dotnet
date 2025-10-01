namespace Capstone.LMS.Application.Dtos.Auth
{
    public class GetRefreshTokenResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
