namespace Capstone.LMS.Domain.Constants
{
    public abstract class LibraryPolicy
    {
        public abstract class Borrowing
        {
            public const int MaxBooksPerBorrower = 3;
            public const int LoanPeriodDays = 14;
        }

        public abstract class Renewal
        {
            public const int MaxRenewals = 3;
        }

        public abstract class Overdue
        {
            public const decimal FinePerDay = 1; // $
            public const decimal MaxFineAmount = 10; // $
        }

        public abstract class Reservation
        {
            public const int ReservationLimit = 2;
        }
    }

}
