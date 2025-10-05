using Capstone.LMS.Application.Dtos.Book;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System;

namespace Capstone.LMS.Application.Commands.Book
{
    public record RequestBorrowBookCommand(
        Guid UserId,
        Guid BookId) : IRequest<Result<BorrowBookResponseDto>>;
}
