using IntercambioGenebraAPI.Domain.Entities;
using IntercambioGenebraAPI.Infrastructure;
using IntercambioGenebraAPI.Infrastructure.Repositories;

namespace IntercambioGenebraAPI.Tests.Utils.Factories.Entities
{
    public static class CategoryFactory
    {
        public static Category CreateTestCategory(AppDbContext _context)
        {
            var _categoryRepository = new CategoryRepository(_context);
            const string testCategoryName = "Software";
            var testCategory = new Category(testCategoryName);
            
            _categoryRepository.Insert(testCategory);
            _categoryRepository.Save();

            return testCategory;
        }
        public static Category CreateTestCategoryWithProduct(AppDbContext _context)
        {
            var _categoryRepository = new CategoryRepository(_context);
            var _productRepository = new ProductRepository(_context);
            const string testCategoryName = "Software";
            var testCategory = new Category(testCategoryName);
            
            _categoryRepository.Insert(testCategory);
            _categoryRepository.Save();

            const string testProductName = "Visual Studio";
            var testProduct = new Product(testProductName, testCategory);
            
            _productRepository.Insert(testProduct);
            _productRepository.Save();

            return testCategory;
        }
    }
}
