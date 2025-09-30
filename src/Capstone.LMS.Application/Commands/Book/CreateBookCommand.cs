using Capstone.LMS.Application.Dtos.Book;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System;

namespace Capstone.LMS.Application.Commands.Book
{
    public record CreateBookCommand(
        string Title,
        string Summary,
        string Isbn,
        DateTime PublishedOn,
        int TotalCopies,
        Guid GenreId,
        Guid AuthorId) : IRequest<Result<CreateBookResponseDto>>;
}
