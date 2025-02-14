using urlShortener.Models;
namespace urlShortener.Repositories

{
    public interface IUrlRepository
    {
        void Add(Address url);
        Task<bool> ExistsAsync(string fullShortUrl);
    }
}
