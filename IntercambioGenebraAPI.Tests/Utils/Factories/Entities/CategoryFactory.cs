using IntercambioGenebraAPI.Domain.Entities;
using IntercambioGenebraAPI.Infrastructure;
using IntercambioGenebraAPI.Infrastructure.Repositories;

namespace IntercambioGenebraAPI.Tests.Utils.Factories.Entities
{
    public static class CategoryFactory
    {
        public static Category CreateTestCategory(AppDbContext _context, string testCategoryName = "Software")
        {
            var _categoryRepository = new CategoryRepository(_context);
            var testCategory = new Category(testCategoryName);
            
            _categoryRepository.Insert(testCategory);
            _categoryRepository.Save();

            return testCategory;
        }
        public static Category CreateTestCategoryWithProduct(AppDbContext _context, string testCategoryName = "Software", string testProductName = "Visual Studio")
        {
            var _productRepository = new ProductRepository(_context);
            var testCategory = CreateTestCategory(_context, testCategoryName);
            var testProduct = new Product(testProductName, testCategory);
            
            _productRepository.Insert(testProduct);
            _productRepository.Save();

            return testCategory;
        }
    }
}
