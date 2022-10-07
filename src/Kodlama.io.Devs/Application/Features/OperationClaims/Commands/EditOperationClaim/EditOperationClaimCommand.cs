using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Features.ProgrammingLanguages.Commands.EditProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Commands.EditOperationClaim
{
    public class EditOperationClaimCommand:IRequest<EditedOperationClaimDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

       public class EditOperationClaimCommandHandler : IRequestHandler<EditOperationClaimCommand, EditedOperationClaimDto>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

            public EditOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
                _operationClaimBusinessRules = operationClaimBusinessRules;
            }


            public async Task<EditedOperationClaimDto> Handle(EditOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _operationClaimBusinessRules.OperationClaimNameCanNotBeDublicatedWhenInserted(request.Name);


                OperationClaim mappedOperationClaim = _mapper.Map<OperationClaim>(request);
                OperationClaim editedOperationClaim = await _operationClaimRepository.UpdateAsync(mappedOperationClaim);
                EditedOperationClaimDto editedOperationClaimDto = _mapper.Map<EditedOperationClaimDto>(editedOperationClaim);

                return editedOperationClaimDto;
            }
        }
    }
}
