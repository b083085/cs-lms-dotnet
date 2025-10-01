using Capstone.LMS.Domain.Entities;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Authentication
{
    public interface ITokenProvider
    {
        Task<string> CreateAccessTokenAsync(User user);
        string CreateRefreshToken();
    }
}
