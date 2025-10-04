using Capstone.LMS.Application.Dtos.Author;
using Capstone.LMS.Application.Dtos.Genre;
using Capstone.LMS.Domain.Shared;
using MediatR;

namespace Capstone.LMS.Application.Commands.Author
{
    public record CreateAuthorCommand(
        string Name) : IRequest<Result<CreateAuthorResponseDto>>;
}
