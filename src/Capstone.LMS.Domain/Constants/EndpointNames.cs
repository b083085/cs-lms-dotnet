namespace Capstone.LMS.Domain.Constants
{
    public static class EndpointNames
    {
        public static class Auth
        {
            public static readonly string Verify = "Verify";
        }

        public static class User
        {
            public static readonly string GetUser = "GetUser";
        }

        public static class Book
        {
            public static readonly string GetBook = "GetBook";
            public static readonly string GetBookBorrowed = "GetBookBorrowed";
        }

        public static class Genre
        {
            public static readonly string GetGenre = "GetGenre";
        }
        

        public static class Author
        {
            public static readonly string GetAuthor = "GetAuthor";
        }
    }
}
