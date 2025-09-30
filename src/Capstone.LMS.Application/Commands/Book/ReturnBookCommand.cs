using Capstone.LMS.Application.Dtos;
using MediatR;
using System;

namespace Capstone.LMS.Application.Commands.Book
{
    public record ReturnBookCommand(
        Guid BookId) : IRequest<SuccessResponseDto>;
}
