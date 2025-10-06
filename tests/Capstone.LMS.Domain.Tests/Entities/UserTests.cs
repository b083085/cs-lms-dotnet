using Capstone.LMS.Domain.DomainEvents;
using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Enums;
using Capstone.LMS.Domain.ValueObjects;

namespace Capstone.LMS.Domain.Tests.Entities
{
    public class UserTests
    {
        [Fact]
        public void Create_ShouldCreateUserWithCorrectProperties()
        {
            // Arrange
            var id = Guid.NewGuid();
            var firstName = "John";
            var lastName = "Doe";
            var gender = Gender.Create("m").Value;
            var email = "john.doe@example.com";

            // Act
            var user = User.Create(id, firstName, lastName, gender, email);

            // Assert
            Assert.Equal(id, user.Id);
            Assert.Equal(firstName, user.FirstName);
            Assert.Equal(lastName, user.LastName);
            Assert.Equal(gender, user.Gender);
            Assert.Equal(email, user.Email);
            Assert.Equal(email, user.UserName);
            Assert.Equal(ActiveStatus.Active, user.ActiveStatus);
            Assert.Empty(user.BorrowedBooks);
            
            // Verify domain event was raised
            var domainEvents = user.GetDomainEvents();
            Assert.Single(domainEvents);
            Assert.IsType<UserRegisteredDomainEvent>(domainEvents.First());
            Assert.Equal(id, ((UserRegisteredDomainEvent)domainEvents.First()).UserId);
        }

        [Fact]
        public void GetFullName_ShouldReturnCombinedFirstAndLastName()
        {
            // Arrange
            var user = CreateTestUser();
            var expectedFullName = $"{user.FirstName} {user.LastName}";

            // Act
            var fullName = user.GetFullName();

            // Assert
            Assert.Equal(expectedFullName, fullName);
        }

        [Fact]
        public void Active_ShouldSetStatusToActiveAndUpdateActivatedDate()
        {
            // Arrange
            var user = CreateTestUser();
            
            // Act
            user.Active();
            
            // Assert
            Assert.Equal(ActiveStatus.Active, user.ActiveStatus);
            Assert.NotNull(user.ActivatedOnUtc);
            Assert.True(DateTime.UtcNow.AddMinutes(-1) <= user.ActivatedOnUtc);
        }

        [Fact]
        public void Suspend_ShouldSetStatusToSuspendedAndUpdateSuspendedDate()
        {
            // Arrange
            var user = CreateTestUser();
            var reason = "Violation of library rules";
            
            // Act
            user.Suspend(reason);
            
            // Assert
            Assert.Equal(ActiveStatus.Suspended, user.ActiveStatus);
            Assert.NotNull(user.SuspendedOnUtc);
            Assert.True(DateTime.UtcNow.AddMinutes(-1) <= user.SuspendedOnUtc);
            Assert.Equal(reason, user.SuspendReason);
        }

        [Fact]
        public void IsEligible_WithActiveStatusAndNoBorrowedBooks_ShouldReturnTrue()
        {
            // Arrange
            var user = CreateTestUser();
            user.Active();
            
            // Act
            var isEligible = user.IsEligible();
            
            // Assert
            Assert.True(isEligible);
        }

        [Fact]
        public void IsEligible_WithSuspendedStatus_ShouldReturnFalse()
        {
            // Arrange
            var user = CreateTestUser();
            user.Suspend();
            
            // Act
            var isEligible = user.IsEligible();
            
            // Assert
            Assert.False(isEligible);
        }

        [Fact]
        public void Created_ShouldSetCreatedByAndTimestamps()
        {
            // Arrange
            var user = CreateTestUser();
            var createdBy = Guid.NewGuid();
            
            // Act
            user.Created(createdBy);
            
            // Assert
            Assert.Equal(createdBy, user.CreatedBy);
            Assert.Equal(createdBy, user.ModifiedBy);
            Assert.True(DateTime.UtcNow.AddMinutes(-1) <= user.CreatedOnUtc);
        }

        [Fact]
        public void Modified_ShouldUpdateModifiedByAndTimestamp()
        {
            // Arrange
            var user = CreateTestUser();
            var modifiedBy = Guid.NewGuid();
            
            // Act
            user.Modified(modifiedBy);
            
            // Assert
            Assert.Equal(modifiedBy, user.ModifiedBy);
            Assert.True(DateTime.UtcNow.AddMinutes(-1) <= user.ModifiedOnUtc);
        }

        [Fact]
        public void Deleted_ShouldSetDeletedByAndTimestamp()
        {
            // Arrange
            var user = CreateTestUser();
            var deletedBy = Guid.NewGuid();
            
            // Act
            user.Deleted(deletedBy);
            
            // Assert
            Assert.Equal(deletedBy, user.DeletedBy);
            Assert.NotNull(user.DeletedOnUtc);
            Assert.True(DateTime.UtcNow.AddMinutes(-1) <= user.DeletedOnUtc);
        }

        [Fact]
        public void SetPublicId_ShouldGenerateNewGuid()
        {
            // Arrange
            var user = CreateTestUser();
            
            // Act
            user.SetPublicId();
            
            // Assert
            Assert.NotEqual(Guid.Empty, user.PublicId);
        }

        [Fact]
        public void ClearDomainEvents_ShouldRemoveAllEvents()
        {
            // Arrange
            var user = CreateTestUser();
            
            // Act
            user.ClearDomainEvents();
            
            // Assert
            Assert.Empty(user.GetDomainEvents());
        }

        // Helper method to create a test user
        private User CreateTestUser()
        {
            return User.Create(
                Guid.NewGuid(),
                "John",
                "Doe",
                Gender.Create("m").Value,
                "john.doe@example.com"
            );
        }
    }
}