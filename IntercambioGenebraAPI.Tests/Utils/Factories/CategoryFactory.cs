using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntercambioGenebraAPI.Domain.Entities;
using IntercambioGenebraAPI.Domain.Repositories;

namespace IntercambioGenebraAPI.Tests.Utils.Factories
{
    public static class CategoryFactory
    {
        public static Category CreateTestCategory(ICategoryRepository _categoryRepository)
        {
            const string testCategoryName = "Software";
            var testCategory = new Category(testCategoryName);
            _categoryRepository.Insert(testCategory);
            _categoryRepository.Save();

            return testCategory;
        }
        public static Category CreateTestCategoryWithProduct(ICategoryRepository _categoryRepository, IProductRepository _productRepository)
        {
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
