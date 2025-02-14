using Microsoft.EntityFrameworkCore;
using urlShortener.Data;
using urlShortener.Models;

namespace urlShortener.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UrlRepository(AppDbContext context, IConfiguration configuration) 
        {
            _context = context;
            _configuration = configuration;

        }


        public async Task AddUrl(Address url)
        {
            _context.Add(url);
            await _context.SaveChangesAsync();
        }


        public async Task<Address> GetUrl(Guid url)
        {
            return await _context.Addresses.FindAsync(url);
        }

        public async Task<Address> UpdateUrl(Address url)
        {
            _context.Update(url);
            await _context.SaveChangesAsync();
            return url;
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

        public async Task DeleteUrl(Guid id)
        {
            _context.Addresses.Remove(await GetUrl(id));
        }

        public string FormatUrl(string path)
        {
            string? baseUrl = _configuration["UrlShortener:BaseUrl"];
            string newUrl = string.Empty;
            return newUrl = $"{baseUrl}/{path}";

        }

    }
}
