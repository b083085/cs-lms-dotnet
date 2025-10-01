using Capstone.LMS.Application.Commands.Auth;
using Capstone.LMS.Application.Dtos;
using Capstone.LMS.Application.Dtos.Auth;
using Capstone.LMS.Application.Queries.Auth;
using Capstone.LMS.Domain.Shared;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Capstone.LMS.Presentation.Endpoints
{
    public class AuthEndpoints : BaseEndpoints, ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var auth = CreateMapGroup(app, "auth")
                .WithTags("Auth")
                .RequireAuthorization();

            auth.MapPost("login", LoginAsync)
                 .WithSummary("Authenticate user.")
                 .AllowAnonymous();

            auth.MapPost("logout", LogoutAsync)
                 .WithSummary("Sign out the current user.");

            auth.MapPost("signup", SignUpAsync)
                 .WithSummary("Register a new user.")
                 .AllowAnonymous();

            auth.MapPost("verify", VerifyAccountAsync)
                 .WithSummary("Verifies the user account.")
                 .AllowAnonymous();

            auth.MapPost("refresh-token", GetRefreshTokenAsync)
                 .WithSummary("Gets or creates a refresh token.");

            auth.MapDelete("refresh-token/revoke/{userId}", RevokeRefreshTokenAsync)
                 .WithSummary("Revokes the refresh tokens of the user.");
        }

        private static async Task<Results<Ok<LoginResponseDto>, UnauthorizedHttpResult>> LoginAsync(
            IMediator mediator,
            LoginCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ?
                TypedResults.Ok(result.Value) :
                TypedResults.Unauthorized();
        }

        private static async Task<Ok<SuccessResponseDto>> LogoutAsync(
            IMediator mediator,
            LogoutCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return TypedResults.Ok(result);
        }

        private static async Task<Results<Ok, Conflict<Error>>> SignUpAsync(
            IMediator mediator,
            SignUpCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ?
                TypedResults.Ok() :
                TypedResults.Conflict(result.Error);
        }

        private static async Task<Ok<SuccessResponseDto>> VerifyAccountAsync(
            IMediator mediator,
            VerifyAccountCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return TypedResults.Ok(result);
        }

        private static async Task<Results<Ok<GetRefreshTokenResponseDto>, BadRequest<Error>>> GetRefreshTokenAsync(
            IMediator mediator,
            GetRefreshTokenQuery query,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(query, cancellationToken);

            return result.IsSuccess ?
                TypedResults.Ok(result.Value) :
                TypedResults.BadRequest(result.Error);
        }

        private static async Task<Ok<SuccessResponseDto>> RevokeRefreshTokenAsync(
            IMediator mediator,
            Guid userId,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new RevokeRefreshTokenCommand(userId), cancellationToken);
            
            return TypedResults.Ok(result);
        }
    }
}
