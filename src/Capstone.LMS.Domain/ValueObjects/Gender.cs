using Capstone.LMS.Domain.Errors;
using Capstone.LMS.Domain.Primitives;
using Capstone.LMS.Domain.Shared;
using System.Collections.Generic;

namespace Capstone.LMS.Domain.ValueObjects
{
    public sealed class Gender : ValueObject
    {
        public const int MaxLength = 1;

        public string Value { get; }

        private Gender(string value)
        {
            Value = value;
        }

        public static Result<Gender> Create(string gender)
        {
            if (string.IsNullOrWhiteSpace(gender))
            {
                return Result.Failure<Gender>(DomainErrors.Gender.IsEmpty);
            }

            if (gender.Length > MaxLength)
            {
                return Result.Failure<Gender>(DomainErrors.Gender.IsTooLong);
            }

            var genderLowerCase = gender.ToLower();
            if(genderLowerCase != "m" && genderLowerCase != "f")
            {
                return Result.Failure<Gender>(DomainErrors.Gender.IsUnknown);
            }

            return new Gender(gender.ToUpper());
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
