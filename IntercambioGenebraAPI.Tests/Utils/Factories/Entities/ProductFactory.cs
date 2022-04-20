using IntercambioGenebraAPI.Domain.Entities;
using IntercambioGenebraAPI.Infrastructure;
using IntercambioGenebraAPI.Infrastructure.Repositories;

namespace IntercambioGenebraAPI.Tests.Utils.Factories.Entities
{
    public static class ProductFactory
    {
        public static Product CreateTestProduct(AppDbContext _context, string testProductName = "Visual Studio")
        {
            var _productRepository = new ProductRepository(_context);
            const decimal testProductPrice = 10.85m;
            var testCategory = CategoryFactory.CreateTestCategory(_context);
            var testProduct = new Product(testProductName, testCategory, testProductPrice);
            
            _productRepository.Insert(testProduct);
            _productRepository.Save();
            
            return testProduct;
        }
        
        public static Product CreateTestProductOfCategory(AppDbContext _context, Category category, string testProductName = "Visual Studio")
        {
            var _productRepository = new ProductRepository(_context);
            const decimal testProductPrice = 10.85m;
            var testProduct = new Product(testProductName, category, testProductPrice);

            _productRepository.Insert(testProduct);
            _productRepository.Save();

            return testProduct;
        }
    }
}
