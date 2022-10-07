using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.RegisterUser
{
    public class RegisterCommand : IRequest<RegisteredDto>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IpAddress { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IAuthService _authService;
            private readonly AuthBusinessRules _authBusinessRules;

            public RegisterCommandHandler(IUserRepository userRepository, IAuthService authService,AuthBusinessRules authBusinessRules)
            {
                _userRepository = userRepository;
                _authService = authService;
                _authBusinessRules = authBusinessRules; 
            }

            public async Task<RegisteredDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                await _authBusinessRules.EmailConnotBeDublicatedWhenRegistered(request.UserForRegisterDto.Email);
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out var passWordHash, out var passwordSalt);

                ExtendedUser user = new ExtendedUser
                {
                    Email = request.UserForRegisterDto.Email,
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    PasswordHash = passWordHash,
                    PasswordSalt = passwordSalt,
                    Status = true,
                };

                ExtendedUser createdUser = await _userRepository.AddAsync(user);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdUser, request.IpAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                RegisteredDto registeredDto = new()
                {
                    AccessToken = createdAccessToken,
                    RefreshToken = addedRefreshToken,
                };

                return registeredDto;
            }
        }
    }
}
