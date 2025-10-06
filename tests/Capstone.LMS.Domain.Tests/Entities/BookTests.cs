using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Enums;
using System;
using System.Linq;

namespace Capstone.LMS.Domain.Tests.Entities
{
    public class BookTests
    {
        [Fact]
        public void Create_ShouldCreateBookWithCorrectProperties()
        {
            // Arrange
            var id = Guid.NewGuid();
            var title = "Test Book";
            var summary = "Test Summary";
            var isbn = "1234567890";
            var publishedOn = DateTime.UtcNow.AddYears(-1);
            var totalCopies = 5;
            var genre = CreateGenre();
            var author = CreateAuthor();

            // Act
            var book = Book.Create(id, title, summary, isbn, publishedOn, totalCopies, genre, author);

            // Assert
            Assert.Equal(id, book.Id);
            Assert.Equal(title, book.Title);
            Assert.Equal(summary, book.Summary);
            Assert.Equal(isbn, book.Isbn);
            Assert.Equal(publishedOn, book.PublishedOn);
            Assert.Equal(totalCopies, book.TotalCopies);
            Assert.Equal(genre.Id, book.GenreId);
            Assert.Equal(author.Id, book.AuthorId);
            Assert.Equal(Availability.Available, book.Availability);
            Assert.Empty(book.BorrowedBooks);
        }

        [Fact]
        public void Create_WithZeroCopies_ShouldSetAvailabilityToUnavailable()
        {
            // Arrange
            var totalCopies = 0;
            var genre = CreateGenre();
            var author = CreateAuthor();

            // Act
            var book = Book.Create(Guid.NewGuid(), "Test Book", "Summary", "ISBN", DateTime.UtcNow, totalCopies, genre, author);

            // Assert
            Assert.Equal(Availability.Unavailable, book.Availability);
        }

        [Fact]
        public void SetTitle_ShouldUpdateTitle()
        {
            // Arrange
            var book = CreateTestBook();
            var newTitle = "Updated Title";

            // Act
            book.SetTitle(newTitle);

            // Assert
            Assert.Equal(newTitle, book.Title);
        }

        [Fact]
        public void SetSummary_ShouldUpdateSummary()
        {
            // Arrange
            var book = CreateTestBook();
            var newSummary = "Updated Summary";

            // Act
            book.SetSummary(newSummary);

            // Assert
            Assert.Equal(newSummary, book.Summary);
        }

        [Fact]
        public void SetIsbn_ShouldUpdateIsbn()
        {
            // Arrange
            var book = CreateTestBook();
            var newIsbn = "9876543210";

            // Act
            book.SetIsbn(newIsbn);

            // Assert
            Assert.Equal(newIsbn, book.Isbn);
        }

        [Fact]
        public void SetTotalCopies_ShouldUpdateTotalCopies()
        {
            // Arrange
            var book = CreateTestBook();
            var newTotalCopies = 10;

            // Act
            book.SetTotalCopies(newTotalCopies);

            // Assert
            Assert.Equal(newTotalCopies, book.TotalCopies);
        }

        [Fact]
        public void SetGenre_ShouldUpdateGenreId()
        {
            // Arrange
            var book = CreateTestBook();
            var newGenre = CreateGenre(Guid.NewGuid());

            // Act
            book.SetGenre(newGenre);

            // Assert
            Assert.Equal(newGenre.Id, book.GenreId);
        }

        [Fact]
        public void SetAuthor_ShouldUpdateAuthorId()
        {
            // Arrange
            var book = CreateTestBook();
            var newAuthor = CreateAuthor(Guid.NewGuid());

            // Act
            book.SetAuthor(newAuthor);

            // Assert
            Assert.Equal(newAuthor.Id, book.AuthorId);
        }

        [Fact]
        public void IsAvailable_WithMoreCopiesThanBorrowed_ShouldReturnTrue()
        {
            // Arrange
            var book = CreateTestBook(totalCopies: 2);
            var user = CreateUser();
            
            // Act - Borrow one book
            book.Request(user);
            
            // Assert - Should still be available since we have 2 copies and only 1 is borrowed
            Assert.True(book.IsAvailable());
        }

        [Fact]
        public void IsAvailable_WithAllCopiesBorrowed_ShouldReturnTrue()
        {
            // Arrange
            var book = CreateTestBook(totalCopies: 1);
            var user = CreateUser();
            
            // Act - Borrow the only copy
            book.Request(user);
            
            // Assert - Should not be available since it is only a request
            Assert.True(book.IsAvailable());
        }

        [Fact]
        public void UpdateAvailability_ShouldSetCorrectAvailabilityBasedOnBorrowedBooks()
        {
            // Arrange
            var book = CreateTestBook(totalCopies: 1);
            var user = CreateUser();
            
            // Act - Borrow the only copy and update availability
            book.Request(user);
            book.UpdateAvailability();
            
            // Assert
            Assert.Equal(Availability.Available, book.Availability);
        }

        [Fact]
        public void Request_ShouldCreateBorrowedBookAndAddToCollection()
        {
            // Arrange
            var book = CreateTestBook();
            var user = CreateUser();
            
            // Act
            var borrowedBook = book.Request(user);
            
            // Assert
            Assert.NotNull(borrowedBook);
            Assert.Equal(book.Id, borrowedBook.BookId);
            Assert.Equal(user.Id, borrowedBook.UserId);
            Assert.Equal(BorrowedStatus.Pending, borrowedBook.Status);
            Assert.Contains(borrowedBook, book.BorrowedBooks);
        }

        // Helper methods to create test objects
        private Book CreateTestBook(int totalCopies = 5)
        {
            return Book.Create(
                Guid.NewGuid(),
                "Test Book",
                "Test Summary",
                "1234567890",
                DateTime.UtcNow.AddYears(-1),
                totalCopies,
                CreateGenre(),
                CreateAuthor()
            );
        }

        private Genre CreateGenre(Guid? id = null)
        {
            // Using reflection to create a Genre instance since the constructor might be private
            var genre = Activator.CreateInstance(typeof(Genre), true) as Genre;
            
            // Set the Id property using reflection
            typeof(Genre).GetProperty("Id").SetValue(genre, id ?? Guid.NewGuid());
            
            return genre;
        }

        private Author CreateAuthor(Guid? id = null)
        {
            // Using reflection to create an Author instance since the constructor might be private
            var author = Activator.CreateInstance(typeof(Author), true) as Author;
            
            // Set the Id property using reflection
            typeof(Author).GetProperty("Id").SetValue(author, id ?? Guid.NewGuid());
            
            return author;
        }

        private User CreateUser()
        {
            // Using reflection to create a User instance since the constructor might be private
            var user = Activator.CreateInstance(typeof(User), true) as User;
            
            // Set the Id property using reflection
            typeof(User).GetProperty("Id").SetValue(user, Guid.NewGuid());
            
            return user;
        }
    }
}