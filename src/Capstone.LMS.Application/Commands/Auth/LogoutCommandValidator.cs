using FluentValidation;

namespace Capstone.LMS.Application.Commands.Auth
{
    public sealed class LogoutCommandValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.SessionId).NotEmpty();
        }
    }
}
