using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.EditProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.DeleteTechnology;
using Application.Features.Technologies.Commands.EditTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Technology, CreatedTechnologyDto>().ReverseMap();
            CreateMap<Technology, CreateTechnologyCommand>().ReverseMap();

            CreateMap<Technology, EditedTechnologyDto>().ReverseMap();
            CreateMap<Technology, EditTechnologyCommand>().ForMember(c => c.TechnologyId,
                           opt => opt.MapFrom(c => c.Id)).ReverseMap();

            CreateMap<Technology, DeletedTechnologyDto>().ReverseMap();
            CreateMap<Technology, DeleteTechnologyCommand>().ReverseMap();


            CreateMap<Technology, TechnologyGetByIdDto>().ReverseMap();
            CreateMap<Technology, TechnologyListDto>()
                .ForMember(c => c.ProgrammingLanguageName, 
                           opt => opt.MapFrom(c => c.ProgrammingLanguage.LanguageName))
                .ReverseMap();
            CreateMap<IPaginate<Technology>, TechnologyListModel>().ReverseMap();  
        }
    }
}
