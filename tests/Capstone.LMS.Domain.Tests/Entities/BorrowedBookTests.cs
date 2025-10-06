using Capstone.LMS.Domain.Constants;
using Capstone.LMS.Domain.DomainEvents;
using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Enums;

namespace Capstone.LMS.Domain.Tests.Entities
{
    public class BorrowedBookTests
    {
        [Fact]
        public void Create_ShouldCreateBorrowedBookWithPendingStatus()
        {
            // Arrange
            var id = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            // Act
            var borrowedBook = BorrowedBook.Create(id, bookId, userId);

            // Assert
            Assert.Equal(id, borrowedBook.Id);
            Assert.Equal(bookId, borrowedBook.BookId);
            Assert.Equal(userId, borrowedBook.UserId);
            Assert.Equal(BorrowedStatus.Pending, borrowedBook.Status);
            Assert.Null(borrowedBook.BorrowedOnUtc);
            Assert.Null(borrowedBook.DueOnUtc);
            Assert.Null(borrowedBook.ReturnedOnUtc);
            Assert.Equal(string.Empty, borrowedBook.BookCondition);
            Assert.Equal(userId, borrowedBook.CreatedBy);
            Assert.NotEqual(default, borrowedBook.CreatedOnUtc);
        }

        [Fact]
        public void Approve_ShouldUpdateStatusToBorrowed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var approvedBy = Guid.NewGuid();
            var borrowedBook = BorrowedBook.Create(id, bookId, userId);

            // Act
            borrowedBook.Approve(approvedBy);

            // Assert
            Assert.Equal(BorrowedStatus.Borrowed, borrowedBook.Status);
            Assert.Equal(approvedBy, borrowedBook.ApprovedBy);
            Assert.NotNull(borrowedBook.ApprovedOnUtc);
            Assert.NotNull(borrowedBook.BorrowedOnUtc);
            Assert.NotNull(borrowedBook.DueOnUtc);
            
            // Check due date is set correctly based on library policy
            var expectedDueDate = borrowedBook.BorrowedOnUtc.Value.AddDays(LibraryPolicy.Borrowing.LoanPeriodDays);
            Assert.Equal(expectedDueDate.Date, borrowedBook.DueOnUtc.Value.Date);
            
            // Verify domain event was raised
            var domainEvent = borrowedBook.GetDomainEvents().SingleOrDefault() as ApprovedBorrowBookDomainEvent;
            Assert.NotNull(domainEvent);
            Assert.Equal(id, domainEvent.BookBorrowedId);
        }

        [Fact]
        public void Rejected_ShouldUpdateStatusToRejected()
        {
            // Arrange
            var id = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var rejectedBy = Guid.NewGuid();
            var rejectedReason = "Book not available";
            var borrowedBook = BorrowedBook.Create(id, bookId, userId);

            // Act
            borrowedBook.Rejected(rejectedBy, rejectedReason);

            // Assert
            Assert.Equal(BorrowedStatus.Rejected, borrowedBook.Status);
            Assert.Equal(rejectedBy, borrowedBook.RejectedBy);
            Assert.NotNull(borrowedBook.RejectedOnUtc);
            Assert.Equal(rejectedReason, borrowedBook.RejectedReason);
            
            // Verify domain event was raised
            var domainEvent = borrowedBook.GetDomainEvents().SingleOrDefault() as RejectedBorrowBookDomainEvent;
            Assert.NotNull(domainEvent);
            Assert.Equal(id, domainEvent.BookBorrowedId);
        }

        [Fact]
        public void Return_ShouldUpdateStatusToReturned()
        {
            // Arrange
            var id = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var approvedBy = Guid.NewGuid();
            var borrowedBook = BorrowedBook.Create(id, bookId, userId);
            borrowedBook.Approve(approvedBy);
            
            // Clear domain events from approval
            borrowedBook.ClearDomainEvents();

            // Act
            borrowedBook.Return();

            // Assert
            Assert.Equal(BorrowedStatus.Returned, borrowedBook.Status);
            Assert.NotNull(borrowedBook.ReturnedOnUtc);
        }

        [Fact]
        public void Overdue_ShouldUpdateStatusToOverdue()
        {
            // Arrange
            var id = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var borrowedBook = BorrowedBook.Create(id, bookId, userId);

            // Act
            borrowedBook.Overdue();

            // Assert
            Assert.Equal(BorrowedStatus.Overdue, borrowedBook.Status);
        }

        [Fact]
        public void SetBookCondition_ShouldUpdateBookCondition()
        {
            // Arrange
            var id = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var condition = "Slightly damaged";
            var borrowedBook = BorrowedBook.Create(id, bookId, userId);

            // Act
            borrowedBook.SetBookCondition(condition);

            // Assert
            Assert.Equal(condition, borrowedBook.BookCondition);
        }

        [Fact]
        public void IsOverdue_ShouldReturnTrue_WhenCurrentDateIsAfterDueDate()
        {
            // Arrange
            var id = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var borrowedBook = BorrowedBook.Create(id, bookId, userId);
            
            // Use reflection to set DueOnUtc to a past date
            typeof(BorrowedBook).GetProperty("DueOnUtc").SetValue(borrowedBook, DateTime.UtcNow.AddDays(-1));

            // Act & Assert
            Assert.True(borrowedBook.IsOverdue);
        }

        [Fact]
        public void IsOverdue_ShouldReturnFalse_WhenCurrentDateIsBeforeDueDate()
        {
            // Arrange
            var id = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var borrowedBook = BorrowedBook.Create(id, bookId, userId);
            
            // Use reflection to set DueOnUtc to a future date
            typeof(BorrowedBook).GetProperty("DueOnUtc").SetValue(borrowedBook, DateTime.UtcNow.AddDays(1));

            // Act & Assert
            Assert.False(borrowedBook.IsOverdue);
        }
    }
}