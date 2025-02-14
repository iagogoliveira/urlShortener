using Microsoft.AspNetCore.Mvc;
using urlShortener.DTOs;
using urlShortener.Models;
using urlShortener.Services;

namespace urlShortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlController : ControllerBase
    {

        private readonly UrlService _urlService;
        public UrlController(UrlService urlService) 
        { 
            _urlService = urlService;
        }


        [HttpPost("CreateNewUrl")]
        public IActionResult CreateNewUrl([FromBody] CreateNewUrlDto urlDto) 
        {

            if (urlDto == null)
            {
                return BadRequest("Url cannot de null.");
            }

            //TODO - Create a method that generate a new url

            var url = new Address(urlDto.NewUrl, urlDto.OriginalUrl, urlDto.UserId);

            try
            {
                _urlService.CreateNewUrl(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }

            return Ok();
        }



    }
}
