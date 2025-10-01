using Capstone.LMS.Domain.Shared;

namespace Capstone.LMS.Domain.Errors
{
    public static class DomainErrors
    {
        public static class User
        {
            public static readonly Error GenderIsEmpty = new(
                "Gender.Empty",
                "Gender is empty.");

            public static readonly Error GenderIsTooLong = new(
                "Gender.TooLong",
                "Gender is too long.");

            public static readonly Error GenderIsUnknown = new(
                "Gender.Unknown",
                "Gender is unknown.");
        }

        public static class Auth
        {
            public static readonly Error UserNotFound = new(
                "User.NotFound",
                "User not found.");

            public static readonly Error PasswordIsInvalid = new(
                "Password.Invalid",
                "Password is invalid.");

            public static readonly Error RefreshTokenIsExpired = new(
                "RefreshToken.Expired",
                "RefreshToken is expired.");
        }
    }
}
