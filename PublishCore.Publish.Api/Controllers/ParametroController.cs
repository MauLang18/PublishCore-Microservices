using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishCore.Publish.Application.Dtos.Parametro.Request;
using PublishCore.Publish.Application.Interfaces;
using PublishCore.Publish.Infrastructure.Commons.Bases.Request;

namespace PublishCore.Publish.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ParametroController : ControllerBase
    {
        private readonly IParametroApplication _parametroApplication;

        public ParametroController(IParametroApplication parametroApplication)
        {
            _parametroApplication = parametroApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListParametros([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _parametroApplication.ListParametros(filters);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> ParametroById(int id)
        {
            var response = await _parametroApplication.ParametroById(id);

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterParametro([FromBody] ParametroRequestDto requestDto)
        {
            var response = await _parametroApplication.RegisterParametro(requestDto);

            return Ok(response);
        }

        [HttpPut("Edit/{id:int}")]
        public async Task<IActionResult> EditParametro(int id, [FromBody] ParametroRequestDto requestDto)
        {
            var response = await _parametroApplication.EditParametro(id, requestDto);

            return Ok(response);
        }

        [HttpPut("Remove/{id:int}")]
        public async Task<IActionResult> RemoveParametro(int id)
        {
            var response = await _parametroApplication.RemoveParametro(id);

            return Ok(response);
        }
    }
}