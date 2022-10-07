using Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Application.Features.OperationClaims.Commands.DeleteOperationClaim;
using Application.Features.OperationClaims.Commands.EditOperationClaim;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Models;
using Application.Features.OperationClaims.Queries.GetByIdOperationClaim;
using Application.Features.OperationClaims.Queries.GetListOperationClaim;
using Application.Features.ProgrammingLanguages.Commands.EditProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.DeleteTechnology;
using Application.Features.Technologies.Commands.EditTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using Application.Features.Technologies.Queries.GetByIdTechnology;
using Application.Features.Technologies.Queries.GetListTechnology;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : BaseController
    {

        [HttpGet]
        public async Task<ActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOperationClaimQuery getListOperationClaimQuery = new GetListOperationClaimQuery { PageRequest = pageRequest };
            OperationClaimListModel result = await Mediator.Send(getListOperationClaimQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
        {
            CreatedOperationClaimDto result = await Mediator.Send(createOperationClaimCommand);
            return Created("", result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteOperationClaimCommand deleteOperationClaimCommand)
        {
            DeletedOperationClaimDto result = await Mediator.Send(deleteOperationClaimCommand);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditOperationClaimCommand editOperationClaimCommand)
        {
            EditedOperationClaimDto result = await Mediator.Send(editOperationClaimCommand);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByID([FromRoute] GetByIdOperationClaimQuery getByIdOperationClaimQuery)
        {
            OperationClaimGetByIdDto result = await Mediator.Send(getByIdOperationClaimQuery);
            return Ok(result);
        }
    }
}
