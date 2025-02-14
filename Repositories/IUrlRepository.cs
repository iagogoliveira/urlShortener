using urlShortener.Models;
namespace urlShortener.Repositories

{
    public interface IUrlRepository
    {
        Task AddUrl(Address url);
        Task <Address> GetUrl(Guid url);
        Task <Address> GetUrlRedirect(string shortUrl);
        Task<Address> UpdateUrl(Address url);
        Task DeleteUrl(Guid id);
        string FormatUrl(string path); 
        Task<bool> ExistsAsync(string fullShortUrl);
    }
}
