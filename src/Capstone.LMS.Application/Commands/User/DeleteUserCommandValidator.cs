using FluentValidation;

namespace Capstone.LMS.Application.Commands.User
{
    public sealed class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x =>  x.UserId).NotEmpty(); 
        }
    }
}
