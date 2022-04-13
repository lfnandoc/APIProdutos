using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Infra;
using Microsoft.EntityFrameworkCore;

namespace IntercambioGenebraAPI.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        
        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        
        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            var product = await _context
                .Products
                .FirstOrDefaultAsync(product => product.Id == id);
            
            return product;            
        }
        public async Task<List<Product>> GetAllCategoriesAsync()
        {
            var products = await _context
                .Products
                .AsNoTracking()
                .ToListAsync();

            return products;
        }

    }
}
