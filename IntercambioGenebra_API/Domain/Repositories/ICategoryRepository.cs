using IntercambioGenebraAPI.Domain.Entities;

namespace IntercambioGenebraAPI.Domain.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category?> GetCategoryByIdAsync(Guid id);

        Task<List<Category>> GetAllCategories();

        Task<bool> CategoryHasAssociatedProducts(Guid id);
    }
}