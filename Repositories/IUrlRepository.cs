using urlShortener.Models;
namespace urlShortener.Repositories

{
    public interface IUrlRepository
    {
        void Add(Address url);
    }
}
