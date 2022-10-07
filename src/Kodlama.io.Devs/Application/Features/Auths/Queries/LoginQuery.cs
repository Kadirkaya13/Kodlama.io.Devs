using Application.Features.Auths.Dtos;
using Application.Features.Users.Rules;
using Application.GlobalConstants;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Queries
{
    public class LoginQuery:IRequest<RefreshedTokenDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public class LoginQueryHandler : IRequestHandler<LoginQuery, RefreshedTokenDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IAuthService _authService;
            private readonly UserBusinessRules _userBusinessRules;

            public LoginQueryHandler(IUserRepository userRepository, IAuthService authService, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _authService = authService;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<RefreshedTokenDto> Handle(LoginQuery request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserShouldExistWhenRequested(request.Email);

                ExtendedUser user = await _userRepository.GetAsync(u => u.Email == request.Email && u.Status);

                if (!HashingHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                    throw new BusinessException(Messages.WrongInformation);

                AccessToken accessToken = await _authService.CreateAccessToken(user);

                return new RefreshedTokenDto { AccessToken = accessToken };
            }
        }
    }
}
