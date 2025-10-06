using Capstone.LMS.Domain.Constants;
using FluentValidation;
using System.Collections.Generic;

namespace Capstone.LMS.Application.Queries.Dashboard
{
    public sealed class GetDashboardQueryValidator : AbstractValidator<GetDashboardQuery>
    {
        private readonly List<string> _allowedRoles =
        [
            Roles.Administrator.ToLower(),
            Roles.Borrower.ToLower(),
            Roles.Librarian.ToLower()
        ];

        public GetDashboardQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .When(x => x.Role == Roles.Librarian || x.Role == Roles.Borrower);

            RuleFor(x => x.Role)
                .NotEmpty()
                .Must(role => _allowedRoles.Contains(role.ToLower()))
                .WithMessage("Invalid role.");
        }
    }
}
