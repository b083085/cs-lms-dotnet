using Capstone.LMS.Application.Dtos.Book;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.Book
{
    public sealed class GetPopularBooksQueryHandler : IRequestHandler<GetPopularBooksQuery, IEnumerable<GetPopularBookResponseDto>>
    {
        public Task<IEnumerable<GetPopularBookResponseDto>> Handle(GetPopularBooksQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
