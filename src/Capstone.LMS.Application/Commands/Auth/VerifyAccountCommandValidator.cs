using FluentValidation;

namespace Capstone.LMS.Application.Commands.Auth
{
    public sealed class VerifyAccountCommandValidator : AbstractValidator<VerifyAccountCommand>
    {
        public VerifyAccountCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}
