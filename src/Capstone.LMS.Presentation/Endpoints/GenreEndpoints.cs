using Capstone.LMS.Application.Commands.Genre;
using Capstone.LMS.Application.Dtos.Genre;
using Capstone.LMS.Application.Queries.Genre;
using Capstone.LMS.Domain.Constants;
using Capstone.LMS.Domain.Shared;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Capstone.LMS.Presentation.Endpoints
{
    public class GenreEndpoints : BaseEndpoints, ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var genre = CreateMapGroup(app, "genres")
                .WithTags("Genre");

            genre.MapGet("{genreId:guid}", GetGenreAsync)
                 .WithName(EndpointNames.Genre.GetGenre)
                 .WithSummary("Gets the genre details.");

            genre.MapGet("", GetGenresAsync)
                 .WithSummary("Gets the list of genres.");

            genre.MapPost("", CreateGenreAsync)
                 .WithSummary("Creates a genre.");

            genre.MapDelete("{genreId:guid}", DeleteGenreAsync)
                 .WithSummary("Deletes the genre.");

            genre.MapPut("", UpdateGenreAsync)
                 .WithSummary("Updates the genre.");
        }

        private static async Task<Results<Ok<GetGenreResponseDto>, NotFound<Error>>> GetGenreAsync(
            IMediator mediator,
            Guid genreId,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetGenreQuery(genreId), cancellationToken);

            return result.IsSuccess ?
                TypedResults.Ok(result.Value) :
                TypedResults.NotFound(result.Error);
        }

        private static async Task<Ok<IEnumerable<GetGenreResponseDto>>> GetGenresAsync(
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetGenresQuery(), cancellationToken);

            return TypedResults.Ok(result);
        }


        private static async Task<Results<Created<CreateGenreResponseDto>, Conflict<Error>>> CreateGenreAsync(
            IMediator mediator,
            CreateGenreCommand command,
            LinkGenerator links,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ?
                TypedResults.Created(links.GetPathByName(EndpointNames.Genre.GetGenre, new { genreId = result.Value.GenreId }), result.Value) :
                TypedResults.Conflict(result.Error);
        }

        private static async Task<Results<Ok, BadRequest<Error>>> DeleteGenreAsync(
            IMediator mediator,
            Guid genreId,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new DeleteGenreCommand(genreId), cancellationToken);
            
            return result.IsSuccess ?
                TypedResults.Ok() :
                TypedResults.BadRequest(result.Error);
        }

        private static async Task<Results<Ok, BadRequest<Error>>> UpdateGenreAsync(
            IMediator mediator,
            UpdateGenreCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ?
                TypedResults.Ok() :
                TypedResults.BadRequest(result.Error);
        }
    }
}
