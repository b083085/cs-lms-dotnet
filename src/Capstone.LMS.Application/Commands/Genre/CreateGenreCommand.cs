using Capstone.LMS.Application.Dtos.Genre;
using Capstone.LMS.Domain.Shared;
using MediatR;

namespace Capstone.LMS.Application.Commands.Genre
{
    public record CreateGenreCommand(
        string Name) : IRequest<Result<CreateGenreResponseDto>>;
}
