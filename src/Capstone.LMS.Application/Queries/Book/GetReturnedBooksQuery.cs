using Capstone.LMS.Application.Dtos.Book;

namespace Capstone.LMS.Application.Queries.Book
{
    public record GetReturnedBooksQuery() : BaseListQuery<GetBookBorrowedResponseDto>();
}
