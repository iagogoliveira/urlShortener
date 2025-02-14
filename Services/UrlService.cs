using urlShortener.Repositories;
using urlShortener.Models;

namespace urlShortener.Services
{
    public class UrlService
    {
        private readonly IUrlRepository _urlRepository;

        public UrlService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public void CreateNewUrl(Address url)
        {
            _urlRepository.Add(url);
        }
    }
}
