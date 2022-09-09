using Application.Features.ProgrammingLanguages.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Quaries.GetByIdProgrammingLanguage
{
    public class GetByIdProgrammingLanguageQuery:IRequest<ProgrammingLanguageGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdProgrammingLanguageHandler: IRequestHandler<GetByIdProgrammingLanguageQuery, ProgrammingLanguageGetByIdDto>
        {
            private readonly IProgrammingLanguageRepository _ProgrammingLanguageRepository;
            private readonly IMapper _mapper;

            public GetByIdProgrammingLanguageHandler(IProgrammingLanguageRepository ProgrammingLanguageRepository, IMapper mapper)
            {
                _ProgrammingLanguageRepository = ProgrammingLanguageRepository;
                _mapper = mapper;
            }

            public async Task<ProgrammingLanguageGetByIdDto> Handle(GetByIdProgrammingLanguageQuery request, CancellationToken cancellationToken)
            {
                ProgrammingLanguage ProgrammingLanguage = await _ProgrammingLanguageRepository.GetAsync(p => p.Id == request.Id);

                ProgrammingLanguageGetByIdDto ProgrammingLanguageGetByIdDto = _mapper.Map<ProgrammingLanguageGetByIdDto>(ProgrammingLanguage);
                
                return ProgrammingLanguageGetByIdDto;
            }
        }
    }
}
