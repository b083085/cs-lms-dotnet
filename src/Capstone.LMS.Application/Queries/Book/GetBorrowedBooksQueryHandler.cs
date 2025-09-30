using Capstone.LMS.Application.Dtos;
using Capstone.LMS.Application.Dtos.Book;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.Book
{
    public sealed class GetBorrowedBooksQueryHandler : IRequestHandler<GetBorrowedBooksQuery, ListResponseDto<GetBorrowedBookResponseDto>>
    {
        public Task<ListResponseDto<GetBorrowedBookResponseDto>> Handle(GetBorrowedBooksQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
