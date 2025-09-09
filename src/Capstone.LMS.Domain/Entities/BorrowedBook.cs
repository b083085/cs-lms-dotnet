using Capstone.LMS.Domain.Enums;
using Capstone.LMS.Domain.Primitives;
using System;

namespace Capstone.LMS.Domain.Entities
{
    public sealed class BorrowedBook : Entity
    {
        public BorrowedBook()
            : base(Guid.NewGuid())
        {
            
        }

        public Guid BookId { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime IssuedAtUtc { get; private set; }
        public DateTime DueAtUtc { get; private set; }
        public DateTime? ReturnedAtUtc { get; private set; }
        public BorrowedStatus Status { get; private set; }

        public Book Book { get; private set; }
        public User User { get; private set; }

        public void Issued()
        {
            Status = BorrowedStatus.Borrowed;
            IssuedAtUtc = DateTime.UtcNow;
        }

        public void Returned()
        {
            Status = BorrowedStatus.Returned;
            ReturnedAtUtc = DateTime.UtcNow;
        }

        public void Overdue()
        {
            Status = BorrowedStatus.Overdue;
        }

    }
}
