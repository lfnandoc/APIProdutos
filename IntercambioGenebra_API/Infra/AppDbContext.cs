using Microsoft.EntityFrameworkCore;
using IntercambioGenebraAPI.Entities;

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
