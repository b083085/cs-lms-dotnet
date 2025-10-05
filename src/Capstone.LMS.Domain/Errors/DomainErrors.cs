using Capstone.LMS.Domain.Shared;
using Capstone.LMS.Domain.ValueObjects;

namespace Capstone.LMS.Domain.Errors
{
    public static class DomainErrors
    {
        public static class Gender 
        {
            public static readonly Error IsEmpty = new(
                "Gender.Empty",
                "Gender is empty.");

            public static readonly Error IsTooLong = new(
                "Gender.TooLong",
                "Gender is too long.");

            public static readonly Error IsUnknown = new(
                "Gender.Unknown",
                "Gender is unknown.");
        }

        public static class User
        {
            public static readonly Error NotFound = new(
                "User.NotFound",
                "User not found.");

            public static readonly Error AlreadyConfirmed = new(
                "User.AlreadyConfirmed",
                "User is already confirmed.");
        }

        public static class Password
        {
            public static readonly Error IsInvalid = new(
                "Password.Invalid",
                "Password is invalid.");
        }

        public static class RefreshToken
        {
            public static readonly Error IsExpired = new(
                "RefreshToken.Expired",
                "RefreshToken is expired.");
        }

        public static class Genre
        {
            public static readonly Error AlreadyExist = new(
                "Genre.AlreadyExist",
                "Genre already exist.");

            public static readonly Error NotFound = new(
                "Genre.NotFound",
                "Genre not found.");
        }

        public static class Author
        {
            public static readonly Error AlreadyExist = new(
                "Author.AlreadyExist",
                "Author already exist.");

            public static readonly Error NotFound = new(
                "Author.NotFound",
                "Author not found.");
        }

        public static class Book
        {
            public static readonly Error AlreadyExist = new(
                "Book.AlreadyExist",
                "Book already exist.");

            public static readonly Error NotFound = new(
                "Book.NotFound",
                "Book not found.");

            public static readonly Error IsUnavailable = new(
                "Book.IsUnavailable",
                "Book is unavailable.");
        }

        public static class BookBorrowed
        {
            public static readonly Error NotFound = new(
                "BookBorrowed.NotFound",
                "Book borrowed not found.");
        }
    }
}
