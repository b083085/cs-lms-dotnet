using Capstone.LMS.Application.Dtos;
using Capstone.LMS.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.LMS.Application.Commands.Auth
{
    public sealed class RevokeRefreshTokenCommandHandler(
        IRefreshTokenRepository refreshTokenRepository,
        IHttpContextAccessor httpContextAccessor) : IRequestHandler<RevokeRefreshTokenCommand, SuccessResponseDto>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        private Guid GetCurrentUserId()
        {
            return Guid.TryParse(
                _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier),
                out Guid parsed)
                ? parsed : Guid.Empty;
        }

        public async Task<SuccessResponseDto> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var response = new SuccessResponseDto();

            if (request.UserId != GetCurrentUserId())
            {
                response.Failure();
            }

            await _refreshTokenRepository.DeleteAllAsync(r => r.UserId == request.UserId, cancellationToken);

            response.Success();

            return response;
        }
    }
}
