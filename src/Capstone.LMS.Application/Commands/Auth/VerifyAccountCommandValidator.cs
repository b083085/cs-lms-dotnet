using FluentValidation;

namespace Capstone.LMS.Application.Commands.Auth
{
    public sealed class VerifyAccountCommandValidator : AbstractValidator<VerifyAccountCommand>
    {
        public VerifyAccountCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
