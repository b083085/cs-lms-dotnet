using Capstone.LMS.Domain.Shared;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Capstone.LMS.Domain.Extensions
{
    public static class IdentityExtensions
    {
        public static Result<T> Failure<T>(this IdentityResult identityResult)
        {
            var error = identityResult.GetFirstError();

            return error is null ?
                default :
                Result.Failure<T>(new(error.Code, error.Description));
        }

        public static Result Failure(this IdentityResult identityResult)
        {
            var error = identityResult.GetFirstError();

            return error is null ?
                default :
                Result.Failure(new(error.Code, error.Description));
        }

        public static IdentityError GetFirstError(this IdentityResult identityResult)
        {
            return identityResult.Errors.Any() ?
                identityResult.Errors.First() :
                null;
        }
    }
}
