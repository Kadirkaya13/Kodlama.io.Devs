using Application.Features.OperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Commands.EditUserOperationClaim
{
    public class EditUserOperationClaimCommand : IRequest<EditedOperationClaimDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public int OperationClaimId { get; set; }

        public class EditUserOperetionClaimCommandHandler : IRequestHandler<EditUserOperationClaimCommand, EditedOperationClaimDto>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

            public EditUserOperetionClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<EditedOperationClaimDto> Handle(EditUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _userOperationClaimBusinessRules.UserCanNotTakeSameClaimIfAlreadyTaken(request.UserId,request.OperationClaimId);

                UserOperationClaim mappedUserOperetionClaim = _mapper.Map<UserOperationClaim>(request);
                UserOperationClaim editedUserOperationClaim = await _userOperationClaimRepository.UpdateAsync(mappedUserOperetionClaim);
                EditedOperationClaimDto editedOperationClaimDto = _mapper.Map<EditedOperationClaimDto>(editedUserOperationClaim);

                return editedOperationClaimDto;
            }
        }
    }
}
