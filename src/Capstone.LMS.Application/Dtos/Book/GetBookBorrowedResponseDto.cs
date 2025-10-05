using Capstone.LMS.Domain.Enums;
using System;

namespace Capstone.LMS.Application.Dtos.Book
{
    public class GetBookBorrowedResponseDto
    {
        public Guid BookBorrowedId { get; set; }
        public GetBookItemResponseDto Book { get; set; }
        public BorrowedStatus Status { get; set; }
        public string ApproverName { get; set; }
        public DateTime? ApprovedOnUtc { get; set; }
        public DateTime? BorrowedOnUtc { get; set; }
        public DateTime? DueOnUtc { get; set; }
        public DateTime? ReturnedOnUtc { get; set; }
    }
}
