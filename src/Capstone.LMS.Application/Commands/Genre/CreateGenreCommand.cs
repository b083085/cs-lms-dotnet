using Capstone.LMS.Application.Dtos.Book;
using Capstone.LMS.Application.Dtos.Genre;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System;

namespace Capstone.LMS.Application.Commands.Genre
{
    public record CreateGenreCommand(
        string Title,
        string Summary,
        string Isbn,
        DateTime PublishedOn,
        int TotalCopies,
        Guid GenreId,
        Guid AuthorId) : IRequest<Result<CreateGenreResponseDto>>;
}
