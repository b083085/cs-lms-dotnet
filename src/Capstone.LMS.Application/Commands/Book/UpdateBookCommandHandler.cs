using Capstone.LMS.Application.Dtos.Book;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Book
{
    public sealed class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Result<UpdateBookResponseDto>>
    {
        public Task<Result<UpdateBookResponseDto>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
