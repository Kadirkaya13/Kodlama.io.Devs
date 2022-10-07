using Application.Features.UserOperationClaims.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Queries.GetByIdUserOperationClaim
{
    public class GetByIdUserOperationClaimQuery:IRequest<UserOperationClaimGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdUserOperationClaimHandler : IRequestHandler<GetByIdUserOperationClaimQuery, UserOperationClaimGetByIdDto>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;

            public GetByIdUserOperationClaimHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
            }

            public async Task<UserOperationClaimGetByIdDto> Handle(GetByIdUserOperationClaimQuery request, CancellationToken cancellationToken)
            {
                UserOperationClaim userOperationClaim = await _userOperationClaimRepository.GetAsync(u => u.Id == request.Id);

                UserOperationClaimGetByIdDto userOperationClaimGetByIdDto = _mapper.Map<UserOperationClaimGetByIdDto>(userOperationClaim);

                return userOperationClaimGetByIdDto;
            }
        }
    }
}
