using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishCore.Auth.Application.Dtos.Usuario.Request;
using PublishCore.Auth.Application.Interfaces;

namespace PublishCore.Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthApplication _authApplication;

        public AuthController(IAuthApplication authApplication)
        {
            _authApplication = authApplication;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] TokenRequestDto requestDto)
        {
            var response = await _authApplication.Login(requestDto);

            return Ok(response);
        }
    }
}