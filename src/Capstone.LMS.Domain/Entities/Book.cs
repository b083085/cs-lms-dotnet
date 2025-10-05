using Capstone.LMS.Domain.Enums;
using Capstone.LMS.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Capstone.LMS.Domain.Entities
{
    public sealed class Book : AggregateRoot
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
            Author author,
            Availability availability)
            : base(id)
        {
            Title = title;
            Summary = summary;
            Isbn = isbn;
            PublishedOn = publishedOn;
            TotalCopies = totalCopies;
            GenreId = genre.Id;
            AuthorId = author.Id;
            Availability = availability;
        }

        public string Title { get; private set; }
        public string Summary { get; private set; }
        public string Isbn { get; private set; }
        public DateTime PublishedOn { get; private set; }
        public Guid GenreId { get; private set; }
        public int TotalCopies { get; private set; }
        public Guid AuthorId { get; private set; }
        public Availability Availability { get; private set; }
        public Author Author { get; private set; }
        public Genre Genre { get; private set; }

        public IReadOnlyList<BorrowedBook> BorrowedBooks => [.. _borrowedBooks];

        public void SetTitle(string title) => Title = title;
        public void SetSummary(string summary) => Summary = summary;
        public void SetIsbn(string isbn) => Isbn = isbn;    
        public void SetPublishedOn(DateTime publishedOn) => PublishedOn = publishedOn;
        public void SetTotalCopies(int totalCopies) => TotalCopies = totalCopies;
        public void SetAvailability(Availability availability) => Availability = availability;

        public void SetGenre(Genre genre)
        {
            if(genre.Id != GenreId)
            {
                GenreId = genre.Id;
            }
        }
        public void SetAuthor(Author author)
        {
            if(author.Id != AuthorId)
            {
                AuthorId = author.Id;
            }
        }
        public void UpdateAvailability()
        {
            Availability = IsAvailable() ?
                Availability.Available :
                Availability.Unavailable;
        }
        public bool IsAvailable() => TotalCopies > _borrowedBooks.Count(p => p.Status == Enums.BorrowedStatus.Borrowed);

        public BorrowedBook Request(User user)
        {
            // this serves as a request to borrow the book.
            // if librarian approves the request, the issued and due are updated.
            var borrowedBook = BorrowedBook.Create(
                Guid.NewGuid(), 
                this.Id, 
                user.Id);

            _borrowedBooks.Add(borrowedBook);

            return borrowedBook;
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
                author,
                totalCopies > 0 ? Availability.Available : Availability.Unavailable);

            return book;
        }

    }
}
