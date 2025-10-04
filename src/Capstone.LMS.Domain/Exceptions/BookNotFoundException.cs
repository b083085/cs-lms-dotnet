using System;

namespace Capstone.LMS.Domain.Exceptions
{
    public sealed class BookNotFoundException : DomainException
    {
        public BookNotFoundException(Guid bookId) 
            : base(string.Format("Book not found: {0}", bookId))
        {
            
        }
    }
}
