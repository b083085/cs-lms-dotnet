using Capstone.LMS.Domain.Constants;
using Capstone.LMS.Domain.Enums;
using Capstone.LMS.Domain.Primitives;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.LMS.Domain.Entities
{
    public sealed class BorrowedBook : Entity
    {
        private BorrowedBook() 
        { 
        }

        private BorrowedBook(
            Guid id,
            Guid bookId,
            Guid userId,
            DateTime? borrowedOnUtc,
            DateTime? dueOnUtc,
            DateTime? returnedOnUtc,
            BorrowedStatus status,
            string bookCondition)
            : base(id)
        {
            BookId = bookId;
            UserId = userId;
            BorrowedOnUtc = borrowedOnUtc;
            DueOnUtc = dueOnUtc;
            ReturnedOnUtc = returnedOnUtc;
            Status = status;
            BookCondition = bookCondition;
        }

        public Guid BookId { get; private set; }
        public Guid UserId { get; private set; }
        public Guid? ApprovedBy { get; private set; }
        public DateTime? ApprovedOnUtc { get; private set; }
        public DateTime? BorrowedOnUtc { get; private set; }
        public DateTime? DueOnUtc { get; private set; }
        public DateTime? ReturnedOnUtc { get; private set; }
        public BorrowedStatus Status { get; private set; }
        public string BookCondition { get; private set; }

        [NotMapped]
        public bool IsOverdue => DateTime.UtcNow > DueOnUtc;

        public Book Book { get; private set; }
        public User User { get; private set; }

        public void Approve(Guid approvedBy)
        {
            ApprovedBy = approvedBy;
            ApprovedOnUtc = DateTime.UtcNow;

            BorrowedOnUtc = DateTime.UtcNow;
            DueOnUtc = DateTime.UtcNow.AddDays(LibraryPolicy.Borrowing.LoanPeriodDays);

            Status = BorrowedStatus.Borrowed;
        }

        public void Return()
        {
            ReturnedOnUtc = DateTime.UtcNow;
            Status = BorrowedStatus.Returned;
        }

        public void Overdue()
        {
            Status = BorrowedStatus.Overdue;
        }

        public void SetBookCondition(string condition) => BookCondition = condition;


        public static BorrowedBook Create(
            Guid id,
            Guid bookId,
            Guid userId,
            DateTime? borrowedOnUtc,
            DateTime? dueOnUtc,
            DateTime? returnedOnUtc,
            BorrowedStatus status,
            string bookCondition)
        {
            var borrowedBook = new BorrowedBook(
                id,
                bookId,
                userId,
                borrowedOnUtc,
                dueOnUtc,
                returnedOnUtc,
                status,
                bookCondition);

            borrowedBook.Created(userId);

            return borrowedBook;
        }

    }
}
