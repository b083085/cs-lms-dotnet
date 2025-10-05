using Capstone.LMS.Application.Dtos.User;
using Capstone.LMS.Domain.Errors;
using Capstone.LMS.Domain.Repositories;
using Capstone.LMS.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.User
{
    public sealed class GetUserQueryHandler(
        IUserRepository userRepository,
        ILogger<GetUserQueryHandler> logger) : 
        IRequestHandler<GetUserQuery, Result<GetUserResponseDto>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<GetUserQueryHandler> _logger = logger;

        public async Task<Result<GetUserResponseDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(p => p.Id == request.UserId, cancellationToken);
            if (user is null)
            {
                return Result.Failure<GetUserResponseDto>(DomainErrors.User.NotFound);
            }

            return new GetUserResponseDto
            {
                UserId      =  user.Id,
                UserName    =  user.UserName,
                Email       =  user.Email,
                FirstName   =  user.FirstName,
                LastName    =  user.LastName,
                Gender      =  user.Gender.Value
            };
        }
    }
}
