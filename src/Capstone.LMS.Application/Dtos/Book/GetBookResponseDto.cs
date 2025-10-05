using System.Collections.Generic;

namespace Capstone.LMS.Application.Dtos.Book
{
    public class GetBookResponseDto : GetBookItemResponseDto
    {
        public IEnumerable<GetBorrowedBookStatusResponseDto> Borrowers { get; set; }
    }
}
