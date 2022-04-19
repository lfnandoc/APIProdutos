using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntercambioGenebraAPI.Domain.Entities;
using IntercambioGenebraAPI.Domain.Repositories;

namespace IntercambioGenebraAPI.Tests.Utils.Factories
{
    public static class ProductFactory
    {
        public static Product CreateTestProduct(IProductRepository _productRepository, ICategoryRepository _categoryRepository)
        {
            const string testProductName = "";
            const decimal testProductPrice = 10.85m;
            var testCategory = CategoryFactory.CreateTestCategory(_categoryRepository);
            var testProduct = new Product(testProductName, testCategory, testProductPrice);
            
            _productRepository.Insert(testProduct);
            _productRepository.Save();

            return testProduct;
        }
    }
}
