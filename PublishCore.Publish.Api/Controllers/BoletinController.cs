using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishCore.Publish.Application.Dtos.Boletin.Request;
using PublishCore.Publish.Application.Interfaces;
using PublishCore.Publish.Infrastructure.Commons.Bases.Request;

namespace PublishCore.Publish.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BoletinController : ControllerBase
    {
        private readonly IBoletinApplication _boletinApplication;

        public BoletinController(IBoletinApplication boletinApplication)
        {
            _boletinApplication = boletinApplication;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ListBoletin([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _boletinApplication.ListBoletin(filters);

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> BoletinById(int id)
        {
            var response = await _boletinApplication.BoletinById(id);

            return Ok(response);
        }

        [HttpPost("Register")]
        [RequestSizeLimit(52428800)]
        public async Task<IActionResult> RegisterBoletin([FromForm] BoletinRequestDto requestDto)
        {
            var response = await _boletinApplication.RegisterBoletin(requestDto);

            return Ok(response);
        }

        [HttpPut("Edit/{id:int}")]
        [RequestSizeLimit(52428800)]
        public async Task<IActionResult> EditBoletin(int id, [FromForm] BoletinRequestDto requestDto)
        {
            var response = await _boletinApplication.EditBoletin(id, requestDto);

            return Ok(response);
        }

        [HttpPut("Remove/{id:int}")]
        public async Task<IActionResult> RemoveBoletin(int id)
        {
            var response = await _boletinApplication.RemoveBoletin(id);

            return Ok(response);
        }
    }
}