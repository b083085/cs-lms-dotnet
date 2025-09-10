using System;

namespace Capstone.LMS.Domain.Constants
{
    public abstract class Roles
    {
        public static readonly Guid AdministratorId = new("11dc0843-e160-4b1c-a729-7d7422211e0f");
        public static readonly Guid LibrarianId = new("21dfc5b0-f51e-4000-a7b1-5637805cdd8a");
        public static readonly Guid BorrowerId = new("bb6fea6d-d5af-434e-a25f-13c746c80a4f");

        public const string Administrator = "Administrator";
        public const string Librarian = "Librarian";
        public const string Borrower = "Borrower";
    }
}
