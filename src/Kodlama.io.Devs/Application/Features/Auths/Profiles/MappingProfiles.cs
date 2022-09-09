using Application.Features.Users.Commands.EditUser;
using Application.Features.Users.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ExtendedUser, EditedUserDto>().ReverseMap();
            CreateMap<ExtendedUser, EditUserCommand>().ReverseMap();

        }
    }
}
