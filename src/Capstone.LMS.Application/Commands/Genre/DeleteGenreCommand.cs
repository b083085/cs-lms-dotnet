using Capstone.LMS.Domain.Shared;
using MediatR;
using System;

namespace Capstone.LMS.Application.Commands.Genre
{
    public record DeleteGenreCommand(
        Guid GenreId) : IRequest<Result>;
}
