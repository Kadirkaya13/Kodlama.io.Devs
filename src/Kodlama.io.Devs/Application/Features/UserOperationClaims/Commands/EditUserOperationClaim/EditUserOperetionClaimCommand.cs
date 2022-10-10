using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.EditUserOperationClaim
{
    public class EditUserOperationClaimCommand : IRequest<EditedUserOperationClaimDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public int OperationClaimId { get; set; }

        public class EditUserOperetionClaimCommandHandler : IRequestHandler<EditUserOperationClaimCommand, EditedUserOperationClaimDto>
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

            public async Task<EditedUserOperationClaimDto> Handle(EditUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _userOperationClaimBusinessRules.UserCanNotTakeSameClaimIfAlreadyTaken(request.UserId,request.OperationClaimId);

                UserOperationClaim mappedUserOperetionClaim = _mapper.Map<UserOperationClaim>(request);
                UserOperationClaim editedUserOperationClaim = await _userOperationClaimRepository.UpdateAsync(mappedUserOperetionClaim);
                EditedUserOperationClaimDto EditedUserOperationClaimDto = _mapper.Map<EditedUserOperationClaimDto>(editedUserOperationClaim);

                return EditedUserOperationClaimDto;
            }
        }
    }
}
