using System;

namespace Capstone.LMS.Domain.Exceptions
{
    public sealed class BookBorrowedNotFoundException : DomainException
    {
        public BookBorrowedNotFoundException(Guid bookBorrowedId) 
            : base(string.Format("Book Borrowed not found: {0}", bookBorrowedId))
        {
            
        }
    }
}
