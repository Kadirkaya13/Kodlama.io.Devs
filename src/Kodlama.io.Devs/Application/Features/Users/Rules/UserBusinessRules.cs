using Application.Services.Repositories;
using Application.GlobalConstants;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Rules
{
    public class UserBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public UserBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task EmailCanNotBeDuplicatedWhenInserted(string email)
        {
            var result = await _userRepository.GetListAsync(b => b.Email == email);
            if (result.Items.Any()) throw new BusinessException(Messages.EmailAlreadyExist);
        }

        public async Task UserShouldExistWhenRequested(string email)
        {
            var result = await _userRepository.GetListAsync(b => b.Email == email);
            if (!result.Items.Any()) throw new BusinessException(Messages.UserNotFound);
        }
    }
}
