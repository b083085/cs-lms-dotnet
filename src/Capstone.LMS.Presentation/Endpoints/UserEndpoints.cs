using Capstone.LMS.Application.Commands.User;
using Capstone.LMS.Application.Dtos;
using Capstone.LMS.Application.Dtos.User;
using Capstone.LMS.Application.Queries.User;
using Capstone.LMS.Domain.Constants;
using Capstone.LMS.Domain.Shared;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Capstone.LMS.Presentation.Endpoints
{
    public class UserEndpoints : BaseEndpoints, ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var user = CreateMapGroup(app, "users")
                .WithTags("User");

            user.MapGet("{userId}", GetUserAsync)
                 .WithName(EndpointNames.User.GetUser)
                 .WithSummary("Gets the user details.");

            user.MapPost("list", GetUsersAsync)
                 .WithSummary("Gets a list of users.");

            user.MapPost("", CreateUserAsync)
                 .WithSummary("Creates a user.");

            user.MapDelete("{userId}", DeleteUserAsync)
                 .WithSummary("Deletes the user.");

            user.MapPut("", UpdateUserAsync)
                 .WithSummary("Updates the user.");
        }

        private static async Task<Results<Ok<GetUserResponseDto>, NotFound<Error>>> GetUserAsync(
            IMediator mediator,
            Guid userId,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetUserQuery(userId), cancellationToken);

            return result.IsSuccess ?
                TypedResults.Ok(result.Value) :
                TypedResults.NotFound(result.Error); 
        }

        private static async Task<Ok<ListResponseDto<GetUserResponseDto>>> GetUsersAsync(
            IMediator mediator,
            GetUsersQuery command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return TypedResults.Ok(result);
        }

        private static async Task<Results<Created<CreateUserResponseDto>, Conflict<Error>>> CreateUserAsync(
            IMediator mediator,
            CreateUserCommand command,
            LinkGenerator links,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ?
                TypedResults.Created(links.GetPathByName(EndpointNames.User.GetUser, new { userId = result.Value.UserId }), result.Value) :
                TypedResults.Conflict(result.Error);
        }

        private static async Task<Ok<SuccessResponseDto>> DeleteUserAsync(
            IMediator mediator,
            Guid userId,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new DeleteUserCommand(userId), cancellationToken);

            return TypedResults.Ok(result);
        }

        private static async Task<Results<Ok<UpdateUserResponseDto>, BadRequest<Error>>> UpdateUserAsync(
            IMediator mediator,
            UpdateUserCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ?
                TypedResults.Ok(result.Value) :
                TypedResults.BadRequest(result.Error);
        }
    }
}
