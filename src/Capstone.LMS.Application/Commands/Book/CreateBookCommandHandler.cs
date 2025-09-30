using Capstone.LMS.Application.Dtos.Book;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Book
{
    public sealed class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<CreateBookResponseDto>>
    {
        public Task<Result<CreateBookResponseDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
