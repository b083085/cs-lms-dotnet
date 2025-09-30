using Capstone.LMS.Application.Dtos.Book;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.Book
{
    public sealed class GetOverdueBooksQueryHandler : IRequestHandler<GetOverdueBooksQuery, IEnumerable<GetOverdueBookResponseDto>>
    {
        public Task<IEnumerable<GetOverdueBookResponseDto>> Handle(GetOverdueBooksQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
