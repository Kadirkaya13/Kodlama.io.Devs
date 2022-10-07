using Core.Security.Entities;
using Core.Security.JWT;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthService
{
    public interface IAuthService
    {
        Task<AccessToken> CreateAccessToken(ExtendedUser user);
        public Task<RefreshToken> CreateRefreshToken(ExtendedUser user,string ipAddress);
        public Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
    }
}
