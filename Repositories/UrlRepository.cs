using urlShortener.Data;
using urlShortener.Models;

namespace urlShortener.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly AppDbContext _context;

        public UrlRepository(AppDbContext context) {_context = context;}


        private void Add (Address url)
        {
            _context.Add(url);
            _context.SaveChanges();
        }

    }
}
