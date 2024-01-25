using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishCore.Publish.Application.Dtos.ServicioBeneficio.Request;
using PublishCore.Publish.Application.Interfaces;
using PublishCore.Publish.Infrastructure.Commons.Bases.Request;

namespace PublishCore.Publish.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioBeneficioController : ControllerBase
    {
        private readonly IServicioBeneficioApplication _servicioBeneficioApplication;

        public ServicioBeneficioController(IServicioBeneficioApplication servicioBeneficioApplication)
        {
            _servicioBeneficioApplication = servicioBeneficioApplication;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ListServicioBeneficio([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _servicioBeneficioApplication.ListServicioBeneficio(filters);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ServicioBeneficioById(int id)
        {
            var response = await _servicioBeneficioApplication.ServivioBeneficioById(id);

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterServicioBeneficio([FromForm] ServicioBeneficioRequestDto requestDto)
        {
            var response = await _servicioBeneficioApplication.RegisterServicioBeneficio(requestDto);

            return Ok(response);
        }

        [HttpPut("Edit/{id:int}")]
        public async Task<IActionResult> EditServicioBeneficio(int id, [FromForm] ServicioBeneficioRequestDto requestDto)
        {
            var response = await _servicioBeneficioApplication.EditServicioBeneficio(id, requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{id:int}")]
        public async Task<IActionResult> RemoveServicioBeneficio(int id)
        {
            var response = await _servicioBeneficioApplication.RemoveServicioBeneficio(id);

            return Ok(response);
        }
    }
}