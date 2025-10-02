using Capstone.LMS.Application.Dtos.Genre;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.Genre
{
    public sealed class GetGenreQueryHandler : IRequestHandler<GetGenreQuery, Result<GetGenreResponseDto>>
    {
        public Task<Result<GetGenreResponseDto>> Handle(GetGenreQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
