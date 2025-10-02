using Capstone.LMS.Application.Dtos;
using Capstone.LMS.Application.Dtos.Genre;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.Genre
{
    public sealed class GetGenresQueryHandler : IRequestHandler<GetGenresQuery, ListResponseDto<GetGenreResponseDto>>
    {
        public Task<ListResponseDto<GetGenreResponseDto>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
