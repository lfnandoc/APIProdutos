using Microsoft.EntityFrameworkCore;
using System;
using IntercambioGenebraAPI.Infrastructure;

namespace IntercambioGenebraAPI.Tests.Utils.Factories
{
    public static class InMemoryContextFactory
    {
        public static AppDbContext Create()
        {
            var databaseName = Guid.NewGuid().ToString();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
            
            return new AppDbContext(options);
        }
    }
}
