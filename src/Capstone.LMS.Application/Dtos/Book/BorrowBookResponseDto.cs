using Capstone.LMS.Application.Dtos.User;
using System;

namespace Capstone.LMS.Application.Dtos.Book
{
    public class BorrowBookResponseDto
    {
        public Guid BookBorrowedId { get; set; }
        public GetBookItemResponseDto Book {  get; set; }
        public GetUserResponseDto User { get; set; }
    }
}
