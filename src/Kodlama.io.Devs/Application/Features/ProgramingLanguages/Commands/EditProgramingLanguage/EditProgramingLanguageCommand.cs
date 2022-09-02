using Application.Features.ProgramingLanguages.Dtos;
using Application.Features.ProgramingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgramingLanguages.Commands.EditProgramingLanguage
{
    public class EditProgramingLanguageCommand: IRequest<EditedProgramingLanguageDto>
    {
        public int Id { get; set; }
        public string LanguageName { get; set; }

        public class EditProgramingLanguageCommandHandler : IRequestHandler<EditProgramingLanguageCommand, EditedProgramingLanguageDto>
        {
            private readonly IProgramingLanguageRepository _programingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly ProgramingLanguageBusinessRules _programingLanguageBusinessRules;

            public EditProgramingLanguageCommandHandler(IProgramingLanguageRepository programingLanguageRepository, IMapper mapper,ProgramingLanguageBusinessRules programingLanguageBusinessRules)
            {
                _programingLanguageRepository = programingLanguageRepository;
                _mapper = mapper;
                _programingLanguageBusinessRules = programingLanguageBusinessRules;
            }

            public async Task<EditedProgramingLanguageDto> Handle(EditProgramingLanguageCommand request, CancellationToken cancellationToken)
            {
                await _programingLanguageBusinessRules.ProgramingLanguageNameCanNotBeDublicatedWhenInserted(request.LanguageName);

                
                ProgramingLanguage mappedProgramingLanguage = _mapper.Map<ProgramingLanguage>(request);
                ProgramingLanguage editedProgramingLanguage = await _programingLanguageRepository.UpdateAsync(mappedProgramingLanguage);
                EditedProgramingLanguageDto editedProgramingLanguageDto = _mapper.Map<EditedProgramingLanguageDto>(editedProgramingLanguage);

                return editedProgramingLanguageDto;
            }
        }
    }
}
