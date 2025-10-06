using Capstone.LMS.Domain.Errors;
using Capstone.LMS.Domain.ValueObjects;
using System;

namespace Capstone.LMS.Domain.Tests.ValueObjects
{
    public class GenderTests
    {
        [Theory]
        [InlineData("M")]
        [InlineData("F")]
        [InlineData("m")]
        [InlineData("f")]
        public void Create_WithValidGender_ShouldReturnSuccessResult(string genderValue)
        {
            // Act
            var result = Gender.Create(genderValue);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(genderValue.ToUpper(), result.Value.Value);
        }

        [Fact]
        public void Create_WithEmptyGender_ShouldReturnFailureResult()
        {
            // Arrange
            string emptyGender = string.Empty;

            // Act
            var result = Gender.Create(emptyGender);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(DomainErrors.Gender.IsEmpty, result.Error);
        }

        [Fact]
        public void Create_WithNullGender_ShouldReturnFailureResult()
        {
            // Act
            var result = Gender.Create(null);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(DomainErrors.Gender.IsEmpty, result.Error);
        }

        [Fact]
        public void Create_WithTooLongGender_ShouldReturnFailureResult()
        {
            // Arrange
            string tooLongGender = "Male";

            // Act
            var result = Gender.Create(tooLongGender);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(DomainErrors.Gender.IsTooLong, result.Error);
        }

        [Theory]
        [InlineData("X")]
        [InlineData("N")]
        [InlineData("1")]
        [InlineData("*")]
        public void Create_WithUnknownGender_ShouldReturnFailureResult(string unknownGender)
        {
            // Act
            var result = Gender.Create(unknownGender);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(DomainErrors.Gender.IsUnknown, result.Error);
        }

        [Fact]
        public void Equals_WithSameValue_ShouldReturnTrue()
        {
            // Arrange
            var gender1 = Gender.Create("M").Value;
            var gender2 = Gender.Create("M").Value;

            // Act & Assert
            Assert.Equal(gender1, gender2);
        }

        [Fact]
        public void Equals_WithDifferentValue_ShouldReturnFalse()
        {
            // Arrange
            var gender1 = Gender.Create("M").Value;
            var gender2 = Gender.Create("F").Value;

            // Act & Assert
            Assert.NotEqual(gender1, gender2);
        }
    }
}