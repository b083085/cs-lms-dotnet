using Capstone.LMS.Application.Dtos;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System;

namespace Capstone.LMS.Application.Commands.Book
{
    public record BorrowBookCommand(
        Guid UserId,
        Guid BookId) : IRequest<SuccessResponseDto>;
}
