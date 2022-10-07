using Application.Features.ProgrammingLanguages.Commands.EditProgrammingLanguage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Commands.EditOperationClaim
{
    
    public class EditOperationClaimCommandValidator : AbstractValidator<EditOperationClaimCommand>
    {
        public EditOperationClaimCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Name).MinimumLength(1);
        }
    }
}
