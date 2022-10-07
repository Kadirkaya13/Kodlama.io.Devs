using Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Application.Features.OperationClaims.Commands.DeleteOperationClaim;
using Application.Features.OperationClaims.Commands.EditOperationClaim;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Models;
using Application.Features.ProgrammingLanguages.Commands.EditProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;

namespace Application.Features.OperationClaims.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<OperationClaim, CreatedOperationClaimDto>().ReverseMap();
            CreateMap<OperationClaim, CreateOperationClaimCommand>().ReverseMap();

            CreateMap<OperationClaim, EditedOperationClaimDto>().ReverseMap();
            CreateMap<OperationClaim, EditOperationClaimCommand>().ReverseMap();

            CreateMap<OperationClaim, DeletedOperationClaimDto>().ReverseMap();
            CreateMap<OperationClaim, DeleteOperationClaimCommand>().ReverseMap();

            CreateMap<OperationClaim, OperationClaimListDto>().ReverseMap();
            CreateMap<IPaginate<OperationClaim>, OperationClaimListModel>().ReverseMap();
            CreateMap<OperationClaim, OperationClaimGetByIdDto>().ReverseMap();

        }
    }
}
