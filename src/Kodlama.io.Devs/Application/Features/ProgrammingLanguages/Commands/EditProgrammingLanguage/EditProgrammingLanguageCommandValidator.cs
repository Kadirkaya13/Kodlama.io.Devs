using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Commands.EditProgrammingLanguage
{
    public class EditProgrammingLanguageCommandValidator:AbstractValidator<EditProgrammingLanguageCommand>
    {
        public EditProgrammingLanguageCommandValidator()
        {
            RuleFor(c => c.LanguageName).NotEmpty();
            RuleFor(c => c.LanguageName).MinimumLength(1);
        }
    }
}
