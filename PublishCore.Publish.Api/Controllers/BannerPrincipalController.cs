using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishCore.Publish.Application.Dtos.BannerPrincipal.Request;
using PublishCore.Publish.Application.Interfaces;
using PublishCore.Publish.Infrastructure.Commons.Bases.Request;

namespace PublishCore.Publish.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BannerPrincipalController : ControllerBase
    {
        private readonly IBannerPrincipalApplication _bannerPrincipalApplication;

        public BannerPrincipalController(IBannerPrincipalApplication bannerPrincipalApplication)
        {
            _bannerPrincipalApplication = bannerPrincipalApplication;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ListBannerPrincipal([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _bannerPrincipalApplication.ListBannerPrincipal(filters);

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> BannerPrincipalById(int id)
        {
            var response = await _bannerPrincipalApplication.BannerPrincipalById(id);

            return Ok(response);
        }

        [HttpPost("Register")]
        [RequestSizeLimit(52428800)]
        public async Task<IActionResult> RegisterBannerPrincipal([FromForm] BannerPrincipalRequestDto requestDto)
        {
            var response = await _bannerPrincipalApplication.RegisterBannerPrincipal(requestDto);

            return Ok(response);
        }

        [HttpPut("Edit/{id:int}")]
        [RequestSizeLimit(52428800)]
        public async Task<IActionResult> EditBannerPrincipal(int id, [FromForm] BannerPrincipalRequestDto requestDto)
        {
            var response = await _bannerPrincipalApplication.EditBannerPrincipal(id, requestDto);

            return Ok(response);
        }

        [HttpPut("Remove/{id:int}")]
        public async Task<IActionResult> RemoveBannerPrincipal(int id)
        {
            var response = await _bannerPrincipalApplication.RemoveBannerPrincipal(id);

            return Ok(response);
        }
    }
}