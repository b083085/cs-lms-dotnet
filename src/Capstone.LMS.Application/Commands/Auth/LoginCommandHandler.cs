
using Capstone.LMS.Application.Authentication;
using Capstone.LMS.Application.Dtos.Auth;
using Capstone.LMS.Application.Persistence;
using Capstone.LMS.Domain.Entities;
using Capstone.LMS.Domain.Errors;
using Capstone.LMS.Domain.Repositories;
using Capstone.LMS.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Auth
{
    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponseDto>>
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Domain.Entities.User> _userManager;

        public LoginCommandHandler(
            ITokenProvider tokenProvider,
            IRefreshTokenRepository refreshTokenRepository,
            IUnitOfWork unitOfWork,
            UserManager<Domain.Entities.User> userManager)
        {
            _tokenProvider = tokenProvider;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Result<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user is null || !user.EmailConfirmed)
            {
                return Result.Failure<LoginResponseDto>(DomainErrors.User.NotFound);
            }

            var validPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!validPassword)
            {
                return Result.Failure<LoginResponseDto>(DomainErrors.Password.IsInvalid);
            }

            var accessToken = await _tokenProvider.CreateAccessTokenAsync(user);
            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = _tokenProvider.CreateRefreshToken(),
                ExpiresOnUtc = DateTime.UtcNow.AddDays(7)
            };

            await _refreshTokenRepository.CreateAsync(refreshToken, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var result = Result.Success(new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            });

            return result;
        }
    }
}
