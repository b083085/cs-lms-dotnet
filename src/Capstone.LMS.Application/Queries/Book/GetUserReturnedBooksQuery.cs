using Capstone.LMS.Application.Dtos.Book;
using System;

namespace Capstone.LMS.Application.Queries.Book
{
    public record GetUserReturnedBooksQuery() : BaseListQuery<GetBookBorrowedResponseDto>()
    {
        public Guid UserId { get; init; }
    }
}
