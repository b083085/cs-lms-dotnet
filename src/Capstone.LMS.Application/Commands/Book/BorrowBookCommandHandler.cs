using Capstone.LMS.Application.Dtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Book
{
    public sealed class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, SuccessResponseDto>
    {
        public Task<SuccessResponseDto> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
