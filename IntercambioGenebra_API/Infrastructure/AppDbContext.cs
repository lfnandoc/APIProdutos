﻿using IntercambioGenebraAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntercambioGenebraAPI.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}