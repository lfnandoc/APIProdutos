using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Infra;
using Microsoft.EntityFrameworkCore;

namespace IntercambioGenebraAPI.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category?> GetCategoryByIdAsync(Guid id);

        Task<List<Category>> GetAllCategories();
        
        Task<bool> CategoryHasAssociatedProducts(Guid id);
        
    }
}
