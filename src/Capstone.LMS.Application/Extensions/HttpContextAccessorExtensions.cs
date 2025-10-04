using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Capstone.LMS.Application.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static Guid GetCurrentUserId(this IHttpContextAccessor httpContextAccessor)
        {
            return Guid.TryParse(
                httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier),
                out Guid parsed)
                ? parsed : Guid.Empty;
        }
    }
}
