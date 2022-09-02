using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgramingLanguages.Commands.EditProgramingLanguage
{
    public class EditProgramingLanguageCommandValidator:AbstractValidator<EditProgramingLanguageCommand>
    {
        public EditProgramingLanguageCommandValidator()
        {
            RuleFor(c => c.LanguageName).NotEmpty();
            RuleFor(c => c.LanguageName).MinimumLength(1);
        }
    }
}
