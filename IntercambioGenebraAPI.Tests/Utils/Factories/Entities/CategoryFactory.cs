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
    }
}
