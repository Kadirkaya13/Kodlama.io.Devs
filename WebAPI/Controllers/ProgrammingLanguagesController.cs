using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.EditProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.ProgrammingLanguages.Quaries.GetByIdProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Quaries.GetListProgrammingLanguage;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammingLanguagesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProgrammingLanguageCommand createProgrammingLanguageCommand)
        {
            CreatedProgrammingLanguageDto result = await Mediator.Send(createProgrammingLanguageCommand);
            
            return Created("", result);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProgrammingLanguageCommand deleteProgrammingLanguageCommand)
        {
            DeletedProgrammingLanguageDto result = await Mediator.Send(deleteProgrammingLanguageCommand);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditProgrammingLanguageCommand editProgrammingLanguageCommand)
        {
            EditedProgrammingLanguageDto result = await Mediator.Send(editProgrammingLanguageCommand);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListProgrammingLanguageQuery getListProgrammingLanguageQuery = new() { PageRequest = pageRequest };
            ProgrammingLanguageListModel result = await Mediator.Send(getListProgrammingLanguageQuery);
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByID([FromRoute] GetByIdProgrammingLanguageQuery getByIdProgrammingLanguageQuery)
        {
            ProgrammingLanguageGetByIdDto ProgrammingLanguageGetByIdDto = await Mediator.Send(getByIdProgrammingLanguageQuery);
            return Ok(ProgrammingLanguageGetByIdDto);
        }
    }
}
