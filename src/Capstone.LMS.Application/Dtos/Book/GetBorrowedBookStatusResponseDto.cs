using Capstone.LMS.Application.Dtos.User;
using System;

namespace Capstone.LMS.Application.Dtos.Book
{
    public class GetBorrowedBookStatusResponseDto
    {
        public GetUserResponseDto Borrower { get; set; }
        public DateTime? DueOnUtc { get; set; }
    }
}
