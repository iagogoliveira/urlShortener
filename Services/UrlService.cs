using urlShortener.Repositories;
using urlShortener.Models;
using System;
using System.Text.RegularExpressions;

namespace urlShortener.Services
{
    public class UrlService
    {
        private readonly IUrlRepository _urlRepository;
        private static readonly string pattern = @"(https:\/\/www\.|http:\/\/www\.|https:\/\/|http:\/\/)?[a-zA-Z]{2,}(\.[a-zA-Z]{2,})(\.[a-zA-Z]{2,})?\/[a-zA-Z0-9]{2,}|((https:\/\/www\.|http:\/\/www\.|https:\/\/|http:\/\/)?[a-zA-Z]{2,}(\.[a-zA-Z]{2,})(\.[a-zA-Z]{2,})?)|(https:\/\/www\.|http:\/\/www\.|https:\/\/|http:\/\/)?[a-zA-Z0-9]{2,}\.[a-zA-Z0-9]{2,}\.[a-zA-Z0-9]{2,}(\.[a-zA-Z0-9]{2,})?";

        public UrlService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }
        public async Task CreateNewUrl(Address url)
        {
            if (string.IsNullOrEmpty(url.OriginalUrl))
            {
                throw new InvalidOperationException("Url cannot be null.");
            }

            if (!CheckValidUrl(url.OriginalUrl))
            {
                throw new InvalidOperationException("Invalid URL.");
            }

            try
            {
                url.NewUrl = await GenerateFullUrl();
                await _urlRepository.AddUrl(url);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateUrl(Guid id, string originalUrl, string path)
        {

            if (String.IsNullOrEmpty(originalUrl))
            {
                throw new InvalidOperationException("Url cannot de null.");
            }
            try
            {
                var urlObject = GetUrl(id);

                if (urlObject.Result.OriginalUrl != originalUrl)
                {
                    urlObject.Result.OriginalUrl = originalUrl;

                }

                if (path != null)
                {
                    urlObject.Result.NewUrl = await GenerateCustomPath(path);
                }

                await _urlRepository.UpdateUrl(urlObject.Result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
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

        public bool CheckValidUrl(string url)
        {
            Regex regex = new Regex(pattern);
            return regex.IsMatch(url);
        }
    }
}
