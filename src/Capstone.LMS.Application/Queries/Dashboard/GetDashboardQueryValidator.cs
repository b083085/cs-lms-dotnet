using Capstone.LMS.Domain.Constants;
using FluentValidation;
using System.Collections.Generic;

namespace Capstone.LMS.Application.Queries.Dashboard
{
    public sealed class GetDashboardQueryValidator : AbstractValidator<GetDashboardQuery>
    {
        private readonly List<string> _allowedRoles = new()
        {
            Roles.Administrator.ToLower(),
            Roles.Borrower.ToLower(),
            Roles.Librarian.ToLower()
        };

        public GetDashboardQueryValidator()
        {
            RuleFor(x => x.Role)
                .NotEmpty()
                .Must(role => _allowedRoles.Contains(role.ToLower()))
                .WithMessage("Invalid role.");
        }
    }
}
