using Application.Services.Repositories;
using Application.Utility;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Rules
{
    public class ProgrammingLanguageBusinessRules
    {
        private readonly IProgrammingLanguageRepository _ProgrammingLanguageRepository;
        private readonly ITechnologyRepository _technologyRepository;

        public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository ProgrammingLanguageRepository, ITechnologyRepository technologyRepository)
        {
            _ProgrammingLanguageRepository = ProgrammingLanguageRepository;
            _technologyRepository = technologyRepository;
        }
        public async Task ProgrammingLanguageNameCanNotBeDublicatedWhenInserted(string name)
        {
            IPaginate<ProgrammingLanguage> result = await _ProgrammingLanguageRepository.GetListAsync(p => p.LanguageName == name);
            if (result.Items.Any()) throw new BusinessException(Messages.objectExists);
        }

        public void ProgrammingLanguageShouldExistWhenRequested(ProgrammingLanguage ProgrammingLanguage)
        {
            if (ProgrammingLanguage == null) throw new BusinessException(Messages.parentobjectNotExists);
        }
        //obje silinmeye çalışıldığında alt objelerin olduğunu bildirecek business rule
        public async Task ProgrammingLanguageCantDeletedWhenHasSubObject(int id)
        {
            IPaginate<Technology> result = await _technologyRepository.GetListAsync(p => p.ProgrammingLanguageId == id);
            if (result.Items.Any()) throw new BusinessException(Messages.objectHasExistsSubObject);
        }
         
    }

}
