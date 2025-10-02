using Capstone.LMS.Application.Dtos;
using MediatR;
using System;

namespace Capstone.LMS.Application.Commands.Genre
{
    public record DeleteGenreCommand(
        Guid BookId) : IRequest<SuccessResponseDto>;
}
