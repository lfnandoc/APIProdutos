using Microsoft.EntityFrameworkCore;
using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Configuration;

namespace IntercambioGenebraAPI.Infra
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>        
            optionsBuilder.UseMySQL(AppConfiguration.ConnectionString);
        

    }
}
