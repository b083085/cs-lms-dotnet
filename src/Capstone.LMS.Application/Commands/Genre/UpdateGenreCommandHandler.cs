using Capstone.LMS.Application.Dtos.Genre;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Genre
{
    public sealed class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, Result<UpdateGenreResponseDto>>
    {
        public Task<Result<UpdateGenreResponseDto>> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
