using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Commands.EditProgrammingLanguage
{
    public class EditProgrammingLanguageCommand: IRequest<EditedProgrammingLanguageDto>
    {
        public int Id { get; set; }
        public string LanguageName { get; set; }

        public class EditProgrammingLanguageCommandHandler : IRequestHandler<EditProgrammingLanguageCommand, EditedProgrammingLanguageDto>
        {
            private readonly IProgrammingLanguageRepository _ProgrammingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageBusinessRules _ProgrammingLanguageBusinessRules;

            public EditProgrammingLanguageCommandHandler(IProgrammingLanguageRepository ProgrammingLanguageRepository, IMapper mapper,ProgrammingLanguageBusinessRules ProgrammingLanguageBusinessRules)
            {
                _ProgrammingLanguageRepository = ProgrammingLanguageRepository;
                _mapper = mapper;
                _ProgrammingLanguageBusinessRules = ProgrammingLanguageBusinessRules;
            }

            public async Task<EditedProgrammingLanguageDto> Handle(EditProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                await _ProgrammingLanguageBusinessRules.ProgrammingLanguageNameCanNotBeDublicatedWhenInserted(request.LanguageName);

                
                ProgrammingLanguage mappedProgrammingLanguage = _mapper.Map<ProgrammingLanguage>(request);
                ProgrammingLanguage editedProgrammingLanguage = await _ProgrammingLanguageRepository.UpdateAsync(mappedProgrammingLanguage);
                EditedProgrammingLanguageDto editedProgrammingLanguageDto = _mapper.Map<EditedProgrammingLanguageDto>(editedProgrammingLanguage);

                return editedProgrammingLanguageDto;
            }
           
        }
    }
}
