using Capstone.LMS.Application.Commands.Auth;
using Capstone.LMS.Application.Dtos;
using Capstone.LMS.Application.Dtos.Auth;
using Capstone.LMS.Application.Queries.Auth;
using Capstone.LMS.Domain.Constants;
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
            var group = CreateMapGroup(app, "auth")
                .WithTags("Auth");

            group.MapPost("login", LoginAsync)
                 .WithName("Login")
                 .WithSummary("Authenticate user.");

            group.MapPost("logout", LogoutAsync)
                 .WithName("Logout")
                 .WithSummary("Sign out the current user.")
                 .RequireAuthorization(policy => policy.RequireRole(
                     Roles.Administrator,
                     Roles.Librarian,
                     Roles.Borrower));

            group.MapPost("signup", SignUpAsync)
                 .WithName("SignUp")
                 .WithSummary("Register a new user.");

            group.MapPost("verify", VerifyAccountAsync)
                 .WithName("VerifyAccount")
                 .WithSummary("Verifies the user account.");

            group.MapPost("refresh-token", GetRefreshTokenAsync)
                 .WithName("RefreshToken")
                 .WithSummary("Gets or creates a refresh token.");

            group.MapDelete("refresh-token/revoke/{userId}", RevokeRefreshTokenAsync)
                 .WithName("RevokeRefreshToken")
                 .WithSummary("Revokes the refresh tokens of the user.");
        }

        private static async Task<Results<Ok<LoginResponseDto>, UnauthorizedHttpResult>> LoginAsync(
            IMediator mediator,
            LoginCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsSuccess)
            {
                return TypedResults.Ok(result.Value);
            }

            return TypedResults.Unauthorized();
        }

        private static async Task<Ok<SuccessResponseDto>> LogoutAsync(
            IMediator mediator,
            LogoutCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return TypedResults.Ok(result);
        }

        private static async Task<Results<Created<SignUpResponseDto>, Conflict<Error>>> SignUpAsync(
            IMediator mediator,
            SignUpCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            if (result.IsSuccess)
            {
                return TypedResults.Created("", result.Value);
            }

            return TypedResults.Conflict(result.Error);
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
            if (result.IsSuccess)
            {
                return TypedResults.Ok(result.Value);
            }

            return TypedResults.BadRequest(result.Error);
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
