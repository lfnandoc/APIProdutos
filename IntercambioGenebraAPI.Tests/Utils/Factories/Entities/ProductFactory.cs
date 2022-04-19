using IntercambioGenebraAPI.Domain.Entities;
using IntercambioGenebraAPI.Infrastructure;
using IntercambioGenebraAPI.Infrastructure.Repositories;

namespace IntercambioGenebraAPI.Tests.Utils.Factories.Entities
{
    public static class ProductFactory
    {
        public static Product CreateTestProduct(AppDbContext _context)
        {
            var _productRepository = new ProductRepository(_context);
            const string testProductName = "";
            const decimal testProductPrice = 10.85m;
            var testCategory = CategoryFactory.CreateTestCategory(_context);
            var testProduct = new Product(testProductName, testCategory, testProductPrice);
            
            _productRepository.Insert(testProduct);
            _productRepository.Save();

            return testProduct;
        }
    }
}
