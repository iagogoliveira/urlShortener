using Microsoft.EntityFrameworkCore;
using urlShortener.Models;

namespace urlShortener.Data
{
    public class AppDbContext : DbContext
    {
        DbSet<Address> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=urlShortener;Trusted_Connection=True;TrustServerCertificate=True;",
                            options => options.MigrationsHistoryTable("__MigrationsHistory_urlShortener"));

            base.OnConfiguring(optionsBuilder);
        }
    }
}
