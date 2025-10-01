using FluentValidation;

namespace Capstone.LMS.Application.Commands.Auth
{
    public sealed class RevokeRefreshTokenCommandValidator : AbstractValidator<RevokeRefreshTokenCommand>
    {
        public RevokeRefreshTokenCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
