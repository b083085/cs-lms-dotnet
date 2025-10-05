using Capstone.LMS.Domain.Shared;
using MediatR;
using System;

namespace Capstone.LMS.Application.Commands.Book
{
    public record ApproveBorrowBookCommand(
        Guid BookBorrowedId) : IRequest<Result>;
}
