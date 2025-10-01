using Capstone.LMS.Application.Authentication;
using Capstone.LMS.Application.Dtos.Auth;
using Capstone.LMS.Application.Persistence;
using Capstone.LMS.Domain.Errors;
using Capstone.LMS.Domain.Repositories;
using Capstone.LMS.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Queries.Auth
{
    public sealed class GetRefreshTokenQueryHandler : IRequestHandler<GetRefreshTokenQuery, Result<GetRefreshTokenResponseDto>>
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Domain.Entities.User> _userManager;

        public GetRefreshTokenQueryHandler(
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

        public async Task<Result<GetRefreshTokenResponseDto>> Handle(GetRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository.GetAsync(r => r.Token == request.RefreshToken, cancellationToken);

            if (refreshToken is null || refreshToken.ExpiresOnUtc < DateTime.UtcNow)
            {
                return Result.Failure<GetRefreshTokenResponseDto>(DomainErrors.Auth.RefreshTokenIsExpired);
            }

            refreshToken.Token = _tokenProvider.CreateRefreshToken();
            refreshToken.ExpiresOnUtc = DateTime.UtcNow.AddDays(7);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var accessToken = await _tokenProvider.CreateAccessTokenAsync(refreshToken.User);

            return Result.Success(new GetRefreshTokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            });
        }
    }
}
