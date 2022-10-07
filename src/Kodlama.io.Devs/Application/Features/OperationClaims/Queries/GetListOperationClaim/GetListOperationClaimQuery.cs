using Application.Features.OperationClaims.Models;
using Application.Features.Technologies.Models;
using Application.Features.Technologies.Queries.GetListTechnology;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Queries.GetListOperationClaim
{
    public class GetListOperationClaimQuery: IRequest<OperationClaimListModel>
    {
        public PageRequest PageRequest { get; set; }
        public class GetListOperationClaimQueryHandler : IRequestHandler<GetListOperationClaimQuery, OperationClaimListModel>
        {
            private readonly IMapper _mapper;
            private readonly IOperationClaimRepository _operationClaimRepository;

            public GetListOperationClaimQueryHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository)
            {
                _mapper = mapper;
                _operationClaimRepository = operationClaimRepository;
            }

            public async Task<OperationClaimListModel> Handle(GetListOperationClaimQuery request, CancellationToken cancellationToken)
            {

                IPaginate<OperationClaim> models = await _operationClaimRepository.GetListAsync(index: request.PageRequest.Page,
                                               size: request.PageRequest.PageSize
                                               );

                OperationClaimListModel mappedModels = _mapper.Map<OperationClaimListModel>(models);
                return mappedModels;
            }
        }
    }
}
