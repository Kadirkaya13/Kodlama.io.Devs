using Application.Services.Repositories;
using Core.Security.Entities;
using Core.Security.JWT;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthService(IUserRepository userRepository, ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
        {
            RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
            return addedRefreshToken;
        }

        public async Task<AccessToken> CreateAccessToken(ExtendedUser User)
        {
            User user = await _userRepository.GetAsync(
                predicate: u => u.Id == User.Id,
                include: u => u.Include(p => p.UserOperationClaims)
                    .ThenInclude(o => o.OperationClaim));

            IEnumerable<OperationClaim> operationClaims = user.UserOperationClaims.Select(o => o.OperationClaim);

            AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims.ToList());

            return accessToken;
        }

        public async Task<RefreshToken> CreateRefreshToken(ExtendedUser user, string ipAddress)
        {
            RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress);
            return await Task.FromResult(refreshToken);
        }
    }
}
