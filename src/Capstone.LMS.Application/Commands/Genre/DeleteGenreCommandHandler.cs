using Capstone.LMS.Application.Dtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Genre
{
    public sealed class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, SuccessResponseDto>
    {
        public Task<SuccessResponseDto> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
