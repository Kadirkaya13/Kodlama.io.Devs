using Application.Features.Auths.Commands.RegisterUser;
using Application.Features.Auths.Dtos;
using Application.Features.Auths.Queries;
using Core.Security.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            RegisterCommand registerCommand = new()
            {
                UserForRegisterDto = userForRegisterDto,
                IpAddress = GetIpAddress()
            };


            RegisteredDto result = await Mediator.Send(registerCommand);
            setRefreshTokenToCookie(result.RefreshToken);
            return Created("", result.AccessToken);
        }

        private void setRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new CookieOptions { HttpOnly = true, Expires = DateTime.Now.AddDays(7) };
            Response.Cookies.Append("refreshtoken", refreshToken.Token, cookieOptions);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery)
        {
            RefreshedTokenDto loginDto = await Mediator.Send(loginQuery);
            return Ok(loginDto);
        }
    }
}
