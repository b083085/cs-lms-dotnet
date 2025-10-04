namespace Capstone.LMS.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNotEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsEmpty(this string value)
        {
            return !value.IsNotEmpty();
        }
    }
}
