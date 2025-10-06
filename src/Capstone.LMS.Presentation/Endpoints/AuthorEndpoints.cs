using Capstone.LMS.Application.Commands.Author;
using Capstone.LMS.Application.Dtos.Author;
using Capstone.LMS.Application.Queries.Author;
using Capstone.LMS.Domain.Constants;
using Capstone.LMS.Domain.Shared;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Capstone.LMS.Presentation.Endpoints
{
    public class AuthorEndpoints : BaseEndpoints, ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var genre = CreateMapGroup(app, "authors")
                .WithTags("Author");

            genre.MapGet("{authorId:guid}", GetAuthorAsync)
                 .WithName(EndpointNames.Author.GetAuthor)
                 .WithSummary("Gets the author details.");

            genre.MapGet("", GetAuthorsAsync)
                 .WithSummary("Gets the list of authors.");

            genre.MapPost("", CreateAuthorAsync)
                 .WithSummary("Creates an author.");

            genre.MapDelete("{authorId:guid}", DeleteAuthorAsync)
                 .WithSummary("Deletes the author.");

            genre.MapPut("", UpdateAuthorAsync)
                 .WithSummary("Updates the author.");
        }

        private static async Task<Results<Ok<GetAuthorResponseDto>, NotFound<Error>>> GetAuthorAsync(
            IMediator mediator,
            Guid authorId,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAuthorQuery(authorId), cancellationToken);

            return result.IsSuccess ?
                TypedResults.Ok(result.Value) :
                TypedResults.NotFound(result.Error);
        }

        private static async Task<Ok<IEnumerable<GetAuthorResponseDto>>> GetAuthorsAsync(
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAuthorsQuery(), cancellationToken);

            return TypedResults.Ok(result);
        }

        private static async Task<Results<Created<CreateAuthorResponseDto>, Conflict<Error>>> CreateAuthorAsync(
            IMediator mediator,
            CreateAuthorCommand command,
            LinkGenerator links,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ?
                TypedResults.Created(links.GetPathByName(EndpointNames.Author.GetAuthor, new { authorId = result.Value.AuthorId }), result.Value) :
                TypedResults.Conflict(result.Error);
        }

        private static async Task<Results<Ok, BadRequest<Error>>> DeleteAuthorAsync(
            IMediator mediator,
            Guid authorId,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new DeleteAuthorCommand(authorId), cancellationToken);
            
            return result.IsSuccess ?
                TypedResults.Ok() :
                TypedResults.BadRequest(result.Error);
        }

        private static async Task<Results<Ok, BadRequest<Error>>> UpdateAuthorAsync(
            IMediator mediator,
            UpdateAuthorCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ?
                TypedResults.Ok() :
                TypedResults.BadRequest(result.Error);
        }
    }
}
