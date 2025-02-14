using urlShortener.Repositories;
using urlShortener.Models;

namespace urlShortener.Services
{
    public class UrlService
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IConfiguration _configuration;

        public UrlService(IUrlRepository urlRepository, IConfiguration configuration)
        {
            _urlRepository = urlRepository;
            _configuration = configuration;
        }

        public async Task CreateNewUrl(Address url)
        {
            string baseUrl = _configuration["UrlShortener:BaseUrl"];

            string shortUrl;
            string fullShortUrl;

            do
            {
                shortUrl = GenerateShortUrl();
                fullShortUrl = $"{baseUrl}/{shortUrl}";
            }
            while (await _urlRepository.ExistsAsync(fullShortUrl));

            url.NewUrl = fullShortUrl;

            _urlRepository.Add(url);
        }


        private string GenerateShortUrl()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
