using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using urlShortener.DTOs;
using urlShortener.Models;
using urlShortener.Services;

namespace urlShortener.Controllers
{
    [ApiController]
    [Route("")]
    public class UrlController : ControllerBase
    {

        private readonly UrlService _urlService;
        private readonly RequestHandlerService _requestHandlerService;
        public UrlController(UrlService urlService, RequestHandlerService requestHandlerService) 
        { 
            _urlService = urlService;
            _requestHandlerService = requestHandlerService;
        }

        [Authorize]
        [HttpPost("CreateNewUrl")]
        public async Task<IActionResult> CreateNewUrl([FromBody] CreateNewUrlDto urlDto) 
        {
            var url = new Address(Guid.NewGuid(), urlDto.OriginalUrl, urlDto.UserId);
            return await _requestHandlerService.HandleRequest(() => _urlService.CreateNewUrl(url));
        }

        [Authorize]
        [HttpPut("UpdateUrl")]
        public async Task<IActionResult> UpdateUrl([FromBody] UpdateUrlDto urlDto)
        {
            return await _requestHandlerService.HandleRequest(() => _urlService.UpdateUrl(urlDto.Id, urlDto.OriginalUrl, urlDto.NewPath));
        }

        [Authorize]
        [HttpDelete("DeleteUrl")]
        public async Task<IActionResult> DeleteUrl([FromBody] DeleteUrlDto urlDto)
        {
            return await _requestHandlerService.HandleRequest(() => _urlService.DeleteUrl(urlDto.Id));
        }

        [HttpGet("{redirectUrl}")]
        public async Task<IActionResult> RedirectUrl(string redirectUrl)
        {
            return await _requestHandlerService.HandleRequest(async () =>
            {
                var urlRedirect = await _urlService.GetUrlRedirect(redirectUrl);
                return (IActionResult)Redirect(urlRedirect.OriginalUrl);
            });
        }
    }
}
