using Capstone.LMS.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Capstone.LMS.Domain.Entities
{
    public sealed class Book : Entity
    {
        private List<BorrowedBook> _borrowedBooks = [];

        private Book()
        {
        }

        private Book(
            Guid id,
            string title,
            string summary,
            string isbn,
            DateTime publishedOn,
            int totalCopies,
            Genre genre,
            Author author)
            : base(id)
        {
            Title = title;
            Summary = summary;
            Isbn = isbn;
            PublishedOn = publishedOn;
            TotalCopies = totalCopies;
            
            AddGenre(genre);
            AddAuthor(author);
        }

        public string Title { get; private set; }
        public string Summary { get; private set; }
        public string Isbn { get; private set; }
        public DateTime PublishedOn { get; private set; }
        public Guid GenreId { get; private set; }
        public int TotalCopies { get; private set; }
        public Guid AuthorId { get; private set; }

        [NotMapped]
        public bool IsAvailable => TotalCopies > _borrowedBooks.Count(p => p.Status == Enums.BorrowedStatus.Borrowed);

        public Author Author { get; private set; }
        public Genre Genre { get; private set; }

        public IReadOnlyList<BorrowedBook> BorrowedBooks => [.. _borrowedBooks];

        public BorrowedBook Borrow(User user)
        {
            var borrowedBook = BorrowedBook.Create(
                Guid.NewGuid(), 
                this, 
                user, 
                null, 
                null, 
                null, 
                Enums.BorrowedStatus.Pending, 
                string.Empty);

            _borrowedBooks.Add(borrowedBook);

            return borrowedBook;
        }

        public void AddAuthor(Author author)
        {
            AuthorId = author.Id;
        }

        public void AddGenre(Genre genre)
        {
            GenreId = genre.Id;
        }

        public static Book Create(
            Guid id,
            string title,
            string summary,
            string isbn,
            DateTime publishedOn,
            int totalCopies,
            Genre genre,
            Author author)
        {
            var book = new Book(
                id,
                title,
                summary,
                isbn,
                publishedOn,
                totalCopies,
                genre,
                author);

            return book;
        }

    }
}
