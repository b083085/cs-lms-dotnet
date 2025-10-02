using Capstone.LMS.Application.Dtos.Genre;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Genre
{
    public sealed class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, Result<CreateGenreResponseDto>>
    {
        public Task<Result<CreateGenreResponseDto>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
