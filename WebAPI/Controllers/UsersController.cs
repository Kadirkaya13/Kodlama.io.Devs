using Application.Features.Users.Commands.EditUser;
using Application.Features.Users.Dtos;
using Core.CrossCuttingConcerns.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EditedUserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BusinessProblemDetails))]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] EditUserCommand editUserCommand)
        {
            EditedUserDto updatedUserDto = await Mediator.Send(editUserCommand);

            return Ok(updatedUserDto);
        }
    }
}
