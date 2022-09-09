using Application.Features.ProgrammingLanguages.Commands.EditProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.EditTechnology
{
    public class EditTechnologyCommand : IRequest<EditedTechnologyDto>
    {
        public int TechnologyId { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }

        public class EditTechnologyCommandHandler : IRequestHandler<EditTechnologyCommand, EditedTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public EditTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<EditedTechnologyDto> Handle(EditTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _technologyBusinessRules.TechnologyNameCanNotBeDuplicatedWhenInserted(request.Name);


                Technology mappedTechnology = _mapper.Map<Technology>(request);
                Technology editedTechnology = await _technologyRepository.UpdateAsync(mappedTechnology);
                EditedTechnologyDto editedTechnologyDto = _mapper.Map<EditedTechnologyDto>(editedTechnology);

                return editedTechnologyDto;
            }
        }
    }
}
