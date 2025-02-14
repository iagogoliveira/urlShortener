using Microsoft.EntityFrameworkCore;
using urlShortener.Data;
using urlShortener.Models;

namespace urlShortener.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly AppDbContext _context;

        public UrlRepository(AppDbContext context) {_context = context;}


        public void Add (Address url)
        {
            _context.Add(url);
            _context.SaveChanges();
        }

        public async Task<bool> ExistsAsync(string fullShortUrl)
        {
            try
            {
                return await _context.Addresses.AnyAsync(u => u.NewUrl == fullShortUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

    }
}
