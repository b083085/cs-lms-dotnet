using Capstone.LMS.Application.Authentication;
using Capstone.LMS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Capstone.LMS.Infrastructure.Authentication
{
    internal sealed class TokenProvider(
        UserManager<User> userManager,
        IOptions<JwtOptions> jwtOptions) : ITokenProvider
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        public async Task<string> CreateAccessTokenAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var signingKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = 
            [
                new(JwtRegisteredClaimNames.Sub, user.PublicId.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
                ..roles.Select(role => new Claim(ClaimTypes.Role, role))
            ];

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryInMinutes),
                SigningCredentials = credentials,
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience
            };

            var tokenHandler = new Microsoft.IdentityModel.JsonWebTokens.JsonWebTokenHandler();

            var accessToken = tokenHandler.CreateToken(tokenDescriptor);

            return accessToken;
        }

        public string CreateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }
    }
}
