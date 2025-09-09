using Capstone.LMS.Domain.Primitives;
using System;
using System.Collections.Generic;

namespace Capstone.LMS.Domain.Entities
{
    public sealed class Book : Entity
    {
        public Book()
            : base(Guid.NewGuid())
        {
            
        }

        public string Title { get; private set; }
        public string Summary { get; private set; }
        public string Isbn { get; private set; }
        public DateTime PublishedOn { get; private set; }
        public Guid GenreId { get; private set; }
        public int TotalCopies { get; private set; }
        public Guid AuthorId { get; private set; }

        public Author Author { get; private set; }
        public Genre Genre { get; private set; }

        public IReadOnlyList<BorrowedBook> Issues { get; private set; }
    }
}
