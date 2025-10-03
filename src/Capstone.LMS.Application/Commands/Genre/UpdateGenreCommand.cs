using Capstone.LMS.Application.Dtos.Genre;
using Capstone.LMS.Domain.Shared;
using MediatR;
using System;

namespace Capstone.LMS.Application.Commands.Genre
{
    public record UpdateGenreCommand(
        Guid GenreId,
        string Name) : IRequest<Result>;
}
