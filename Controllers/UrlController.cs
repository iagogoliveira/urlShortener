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
        public UrlController(UrlService urlService) 
        { 
            _urlService = urlService;
        }

        [Authorize]
        [HttpPost("CreateNewUrl")]
        public async Task<IActionResult> CreateNewUrl([FromBody] CreateNewUrlDto urlDto) 
        {

            if (urlDto == null)
            {
                return BadRequest("Url cannot de null.");
            }

            var url = new Address(Guid.NewGuid(), urlDto.OriginalUrl, urlDto.UserId);

            try
            {
                await _urlService.CreateNewUrl(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }

            return Ok();
        }

        [Authorize]
        [HttpPut("UpdateUrl")]
        public async Task<IActionResult> UpdateUrl([FromBody] UpdateUrlDto urlDto)
        {

            if (urlDto == null)
            {
                return BadRequest("Url cannot de null.");
            }

            try
            {
                await _urlService.UpdateUrl(urlDto.Id, urlDto.OriginalUrl, urlDto.NewPath);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }

            return Ok();
        }

        [Authorize]
        [HttpDelete("DeleteUrl")]
        public async Task<IActionResult> DeleteUrl([FromBody] DeleteUrlDto urlDto)
        {

            if (urlDto == null)
            {
                return BadRequest("Url cannot de null.");
            }

            try
            {
                await _urlService.DeleteUrl(urlDto.Id);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }

            return Ok();
        }

        [HttpGet("{redirectUrl}")]
        public async Task<IActionResult> RedirectUrl(string redirectUrl)
        {
            if (redirectUrl == null)
            {
                return BadRequest("Url cannot de null.");
            }
            try
            {
                var urlRedirect = await _urlService.GetUrlRedirect(redirectUrl);
                return Redirect(urlRedirect.OriginalUrl);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }



    }
}
