using Capstone.LMS.Application.Dtos.Book;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.Book
{
    public sealed class GetBookQueryHandler : IRequestHandler<GetBookQuery, Result<GetBookResponseDto>>
    {
        public Task<Result<GetBookResponseDto>> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
