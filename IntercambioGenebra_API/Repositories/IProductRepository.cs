using IntercambioGenebraAPI.Entities;

namespace IntercambioGenebraAPI.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product?> GetProductByIdAsync(Guid id);

        Task<List<Product>> GetAllProductsAsync();

        Task<List<Product>> GetProductsByCategoryIdAsync(Guid id);

    }
}
