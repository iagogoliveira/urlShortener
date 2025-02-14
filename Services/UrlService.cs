using urlShortener.Repositories;
using urlShortener.Models;
using System;

namespace urlShortener.Services
{
    public class UrlService
    {
        private readonly IUrlRepository _urlRepository;

        public UrlService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }
        public async Task CreateNewUrl(Address url)
        {
            url.NewUrl = await GenerateFullUrl();
            await _urlRepository.AddUrl(url);
        }

        public async Task UpdateUrl(Guid id, string originalUrl, string path)
        {
            var urlObject = GetUrl(id);

            if(urlObject.Result.OriginalUrl != originalUrl)
            {
                urlObject.Result.OriginalUrl = originalUrl;

            }

            if(path != null)
            {
                urlObject.Result.NewUrl = await GenerateCustomPath(path);
            }

            await _urlRepository.UpdateUrl(urlObject.Result);
        }

        public async Task<Address> GetUrl(Guid url)
        {
            return  await _urlRepository.GetUrl(url);
        } 
        public async Task<Address> GetUrlRedirect(string shortUrl)
        {
            var url = _urlRepository.FormatUrl(shortUrl);
            return  await _urlRepository.GetUrlRedirect(url);
        }


        public async Task DeleteUrl(Guid id)
        {
            try
            {
                await _urlRepository.DeleteUrl(id);
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("Url not found.");
            }
        }



        private async Task<string> GenerateCustomPath(string customPath)
        {
            string newUrl = _urlRepository.FormatUrl(customPath);


            if (!await _urlRepository.ExistsAsync(newUrl))
            {
                return newUrl;
            }

            throw new InvalidOperationException("Custom path already exists.");
        }


        private async Task<string> GenerateFullUrl()
        {
            string urlPath;
            string fullUrl;
            do
            {
                urlPath = GenerateUrlPath();
                fullUrl = _urlRepository.FormatUrl(urlPath);
            }
            while (await _urlRepository.ExistsAsync(fullUrl));
            return fullUrl;


        }
        private string GenerateUrlPath()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
