using Application.Features.ProgramingLanguages.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgramingLanguages.Models
{
    public class ProgramingLanguageListModel:BasePageableModel
    {
        public IList<ProgramingLanguageListDto> Items { get; set; }
    }
}
