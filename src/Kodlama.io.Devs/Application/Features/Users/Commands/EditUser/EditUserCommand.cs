using Application.Features.Technologies.Dtos;
using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.EditUser
{
    public class EditUserCommand : IRequest<EditedUserDto>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string GitHubAdress { get; set; }

        public class EditUserCommandHandler : IRequestHandler<EditUserCommand, EditedUserDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly UserBusinessRules _userBusinessRules;
            private readonly IMapper _mapper;

            public EditUserCommandHandler(IUserRepository userRepository, UserBusinessRules userBusinessRules, IMapper mapper)
            {
                _userRepository = userRepository;
                _userBusinessRules = userBusinessRules;
                _mapper = mapper;
            }

            public async Task<EditedUserDto> Handle(EditUserCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserShouldExistWhenRequested(request.Email);

                ExtendedUser user = await _userRepository.GetAsync(u => u.Email == request.Email);

                await _userRepository.UpdateAsync(user);

                EditedUserDto updatedUserDto = _mapper.Map<EditedUserDto>(user);

                return updatedUserDto;
            }
        }
    }
}
