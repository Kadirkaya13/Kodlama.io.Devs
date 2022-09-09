using Application.Features.Auths.Commands.RegisterUser;
using Application.Features.Auths.Dtos;
using Application.Features.Auths.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            RegisteredDto registerDto = await Mediator.Send(registerCommand);
            return Created("", registerDto.AccessToken);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery)
        {
            LoginedDto loginDto = await Mediator.Send(loginQuery);
            return Ok(loginDto);
        }
    }
}
