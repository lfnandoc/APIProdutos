using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Infra;
using Microsoft.EntityFrameworkCore;

namespace IntercambioGenebraAPI.Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
        private readonly AppDbContext _context;
        
        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        
        public async Task<Category?> GetCategoryByIdAsync(Guid id)
        {
            var category = await _context
                .Categories
                .FirstOrDefaultAsync(category => category.Id == id);
            
            return category;            
        }
        
        public async Task<List<Category>> GetAllCategories()
        {
            var categories = await _context
                .Categories
                .AsNoTracking()
                .ToListAsync();

            return categories;
        }

        public async Task<bool> CategoryHasAssociatedProducts(Guid id)
        {
            var anyAssociatedProducts = await _context
                .Products
                .AsNoTracking()
                .AnyAsync(product => product.CategoryId == id);

            return anyAssociatedProducts;
        }

    }
}
