using Capstone.LMS.Application.Dtos;
using MediatR;
using System;

namespace Capstone.LMS.Application.Commands.Book
{
    public record DeleteBookCommand(
        Guid BookId) : IRequest<SuccessResponseDto>;
}
