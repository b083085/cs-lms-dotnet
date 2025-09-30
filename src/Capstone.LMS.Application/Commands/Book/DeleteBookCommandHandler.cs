using Capstone.LMS.Application.Dtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Book
{
    public sealed class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, SuccessResponseDto>
    {
        public Task<SuccessResponseDto> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
