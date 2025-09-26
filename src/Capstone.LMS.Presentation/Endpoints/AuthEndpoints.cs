using Carter;

namespace Capstone.LMS.Presentation.Endpoints
{
    public class AuthEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/auth");

            group.MapPost("/signup", SignUp)
                 .WithName("SignUp")
                 .WithSummary("Register a new user");

            group.MapPost("/login", Login)
                 .WithName("Login")
                 .WithSummary("Authenticate user");

            group.MapPost("/logout", Logout)
                 .WithName("Logout")
                 .WithSummary("Sign out the current user");
        }

        private static async Task<IResult> SignUp(HttpContext context)
        {
            // Example body parsing
            var user = await context.Request.ReadFromJsonAsync<UserDto>();
            // TODO: handle user registration logic
            return Results.Created("/auth/signup", user);
        }

        private static async Task<IResult> Login(HttpContext context)
        {
            var login = await context.Request.ReadFromJsonAsync<LoginDto>();
            // TODO: authentication logic
            return Results.Ok(new { Token = "fake-jwt-token" });
        }

        private static Task<IResult> Logout(HttpContext context)
        {
            // TODO: clear token/session logic
            return Task.FromResult(Results.Ok(new { Message = "Logged out" }));
        }
    }

    // Example DTOs
    public record UserDto(string Email, string Password);
    public record LoginDto(string Email, string Password);
}
