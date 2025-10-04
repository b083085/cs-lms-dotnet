using Capstone.LMS.Domain.Shared;
using MediatR;
using System;

namespace Capstone.LMS.Application.Commands.Author
{
    public record UpdateAuthorCommand(
        Guid AuthorId,
        string Name) : IRequest<Result>;
}
