using Capstone.LMS.Domain.Enums;
using Capstone.LMS.Domain.Primitives;
using Capstone.LMS.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Capstone.LMS.Domain.Entities
{
    public sealed class User : IdentityUser<Guid>, IAudit
    {
        private List<BorrowedBook> _borrowedBooks = [];

        private User()
        {

        }

        private User(
            Guid id,
            string firstName,
            string lastName,
            Gender gender,
            string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Email = email;
            UserName = email;
        }

        public Guid PublicId { get; private set; }  
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Gender Gender { get; private set; }
        public ActiveStatus ActiveStatus { get; private set; }
        public Guid CreatedBy { get; private set; }
        public Guid ModifiedBy { get; private set; }
        public Guid? DeletedBy { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime ModifiedOnUtc { get; private set; }
        public DateTime? DeletedOnUtc { get; private set; }
        public DateTime? ActivatedOnUtc {  get; private set; }
        public DateTime? SuspendedOnUtc {  get; private set; }
        public string SuspendReason {  get; private set; }
        public int BorrowingLimit { get; private set; }

        public IReadOnlyList<BorrowedBook> BorrowedBooks => [.. _borrowedBooks];

        public static User Create(
            Guid id,
            string firstName,
            string lastName,
            Gender gender,
            string email)
        {
            return new User(
                id,
                firstName,
                lastName,
                gender,
                email);
        }

        public void Active()
        {
            ActiveStatus = ActiveStatus.Active;
            ActivatedOnUtc = DateTime.UtcNow;
        }

        public void Suspend(string reason = null)
        {
            ActiveStatus = ActiveStatus.Suspended;
            SuspendedOnUtc = DateTime.UtcNow;
            SuspendReason = reason;
        }

        public bool IsEligible()
        {
            // if status is active and
            // no overdue borrowed books and
            // is not beyond the borrowing limit
            return ActiveStatus == ActiveStatus.Active &&
                !_borrowedBooks.Any(p => p.Status == BorrowedStatus.Overdue) &&
                _borrowedBooks.Count(p => p.Status == BorrowedStatus.Borrowed) <= BorrowingLimit;
        }

        public void Created(Guid createdBy)
        {
            CreatedBy = createdBy;
            ModifiedBy = createdBy;
            CreatedOnUtc = DateTime.UtcNow;
            ModifiedOnUtc = DateTime.UtcNow;
        }

        public void Modified(Guid modifiedBy)
        {
            ModifiedBy = modifiedBy;
            ModifiedOnUtc = DateTime.UtcNow;
        }

        public void Deleted(Guid deletedBy)
        {
            DeletedBy = deletedBy;
            DeletedOnUtc = DateTime.UtcNow;
        }

        public void GeneratePublicId() =>
            PublicId = Guid.NewGuid();
    }
}
