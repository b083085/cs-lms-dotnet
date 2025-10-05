using Capstone.LMS.Application.Dtos.Book;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System;

namespace Capstone.LMS.Application.Queries.Book
{
    public record GetBookBorrowedQuery(Guid BookBorrowedId) : IRequest<Result<GetBookBorrowedResponseDto>>;
}
