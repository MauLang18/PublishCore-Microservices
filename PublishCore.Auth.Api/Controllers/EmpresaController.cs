using Microsoft.AspNetCore.Mvc;
using PublishCore.Auth.Application.Dtos.Empresa.Request;
using PublishCore.Auth.Application.Interfaces;
using PublishCore.Auth.Infrastructure.Commons.Bases.Request;

namespace PublishCore.Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaApplication _empresaApplication;

        public EmpresaController(IEmpresaApplication empresaApplication)
        {
            _empresaApplication = empresaApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListEmpresas([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _empresaApplication.ListEmpresas(filters);
            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectEmpresas()
        {
            var response = await _empresaApplication.ListSelectEmpresa();

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> EmpresaById(int id)
        {
            var response = await _empresaApplication.EmpresaById(id);

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterEmpresa([FromBody] EmpresaRequestDto requestDto)
        {
            var response = await _empresaApplication.RegisterEmpresa(requestDto);

            return Ok(response);
        }

        [HttpPut("Edit/{id:int}")]
        public async Task<IActionResult> EditEmpresa(int id, [FromBody] EmpresaRequestDto requestDto)
        {
            var response = await _empresaApplication.EditEmpresa(id, requestDto);

            return Ok(response);
        }

        [HttpPut("Remove/{id:int}")]
        public async Task<IActionResult> RemoveEmpresa(int id)
        {
            var response = await _empresaApplication.RemoveEmpresa(id);

            return Ok(response);
        }
    }
}