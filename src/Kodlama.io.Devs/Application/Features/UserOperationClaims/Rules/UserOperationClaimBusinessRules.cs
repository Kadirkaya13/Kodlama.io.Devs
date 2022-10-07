using Application.Features.UserOperationClaims.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Rules
{
    public class UserOperationClaimBusinessRules
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        public async Task UserCanNotTakeSameClaimIfAlreadyTaken(int userId,int operationClaimId)
        {
            IPaginate<UserOperationClaim> result = await _userOperationClaimRepository.GetListAsync(u=>u.UserId== userId);

            if (result.Items.Any(x => x.OperationClaimId == operationClaimId)) throw new BusinessException(UserOperationClaimMessages.OperationClaimAlreadyTaken);

        }
    }
}
