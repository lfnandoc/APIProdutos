using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Infra;
using Microsoft.EntityFrameworkCore;

namespace IntercambioGenebraAPI.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product?> GetProductByIdAsync(Guid id);

        Task<List<Product>> GetAllProductsAsync();      

    }
}
