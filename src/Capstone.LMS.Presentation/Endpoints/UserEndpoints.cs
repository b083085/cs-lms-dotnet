using Capstone.LMS.Application.Commands.User;
using Capstone.LMS.Application.Dtos;
using Capstone.LMS.Application.Dtos.User;
using Capstone.LMS.Application.Queries.User;
using Capstone.LMS.Domain.Shared;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.LMS.Presentation.Endpoints
{
    public class UserEndpoints : BaseEndpoints, ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = CreateMapGroup(app, "users")
                .WithTags("User");

            group.MapGet("{userId}", GetUserAsync)
                 .WithName("GetUser")
                 .WithSummary("Gets the user details.");

            group.MapPost("list", GetUsersAsync)
                 .WithName("GetUsers")
                 .WithSummary("Gets a list of users.");

            group.MapPost("", CreateUserAsync)
                 .WithName("CreateUser")
                 .WithSummary("Creates a user.");

            group.MapDelete("{userId}", DeleteUserAsync)
                 .WithName("DeleteUser")
                 .WithSummary("Deletes a user.");

            group.MapPut("", UpdateUserAsync)
                 .WithName("UpdateUser")
                 .WithSummary("Updates the user.");
        }

        private static async Task<Results<Ok<GetUserResponseDto>, NotFound<Error>>> GetUserAsync(
            IMediator mediator,
            Guid userId,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetUserQuery(userId), cancellationToken);
            if (result.IsSuccess)
            {
                return TypedResults.Ok(result.Value);
            }

            return TypedResults.NotFound(result.Error);
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
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsSuccess)
            {
                return TypedResults.Created("", result.Value);
            }

            return TypedResults.Conflict(result.Error);
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
            if (result.IsSuccess)
            {
                return TypedResults.Ok(result.Value);
            }

            return TypedResults.BadRequest(result.Error);
        }
    }
}
