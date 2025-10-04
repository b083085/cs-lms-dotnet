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

            public static readonly Error UserAlreadyConfirmed = new(
                "User.AlreadyConfirmed",
                "User is already confirmed.");
        }

        public static class Genre
        {
            public static readonly Error GenreAlreadyExist = new(
                "Genre.AlreadyExist",
                "Genre already exist.");

            public static readonly Error GenreNotFound = new(
                "Genre.NotFound",
                "Genre not found.");
        }

        public static class Author
        {
            public static readonly Error AuthorAlreadyExist = new(
                "Author.AlreadyExist",
                "Author already exist.");

            public static readonly Error AuthorNotFound = new(
                "Author.NotFound",
                "Author not found.");
        }
    }
}
