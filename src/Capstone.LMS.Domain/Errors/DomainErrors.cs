using Capstone.LMS.Domain.Shared;

namespace Capstone.LMS.Domain.Errors
{
    public static class DomainErrors
    {
        public static class User
        {
            public static readonly Error FirstNameIsEmpty = new(
                "FirstName.Empty",
                "First name is empty.");

            public static readonly Error FirstNameIsTooLong = new(
                "FirstName.TooLong",
                "First name is too long.");

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
    }
}
