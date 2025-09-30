using Capstone.LMS.Application.Dtos;
using Capstone.LMS.Application.Dtos.Book;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.Book
{
    public sealed class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, ListResponseDto<GetBookResponseDto>>
    {
        public Task<ListResponseDto<GetBookResponseDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
