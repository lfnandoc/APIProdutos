using IntercambioGenebraAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntercambioGenebraAPI.Infra
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

    }
}
