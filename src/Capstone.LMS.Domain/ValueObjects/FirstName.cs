using Capstone.LMS.Domain.Errors;
using Capstone.LMS.Domain.Primitives;
using Capstone.LMS.Domain.Shared;
using System.Collections.Generic;

namespace Capstone.LMS.Domain.ValueObjects
{
    public sealed class FirstName : ValueObject
    {
        public const int MaxLength = 50;

        public string Value { get; }

        private FirstName(string value)
        {
            Value = value;
        }

        public static Result<FirstName> Create(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                return Result.Failure<FirstName>(DomainErrors.User.FirstNameIsEmpty);
            }

            if (firstName.Length > MaxLength)
            {
                return Result.Failure<FirstName>(DomainErrors.User.FirstNameIsTooLong);
            }

            return new FirstName(firstName);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
