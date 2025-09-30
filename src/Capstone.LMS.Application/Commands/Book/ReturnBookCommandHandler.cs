using Capstone.LMS.Application.Dtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Book
{
    public sealed class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, SuccessResponseDto>
    {
        public Task<SuccessResponseDto> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
