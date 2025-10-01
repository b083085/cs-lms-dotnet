using FluentValidation;

namespace Capstone.LMS.Application.Queries.Auth
{
    public sealed class GetRefreshTokenQueryValidator : AbstractValidator<GetRefreshTokenQuery>
    {
        public GetRefreshTokenQueryValidator()
        {
            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}
