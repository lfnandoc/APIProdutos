using IntercambioGenebraAPI.Domain.Entities;

namespace IntercambioGenebraAPI.Domain.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product?> GetProductByIdAsync(Guid id);

        Task<List<Product>> GetAllProductsAsync();

        Task<List<Product>> GetProductsByCategoryIdAsync(Guid id);
    }
}