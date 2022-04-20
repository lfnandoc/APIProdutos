using FluentAssertions;
using IntercambioGenebraAPI.Domain.Entities;
using Xunit;

namespace IntercambioGenebraAPI.Tests.Domain.Entities
{
    public class ProductTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            var testName = "Visual Studio";
            var testPrice = 10.85m;
            var testCategory = new Category("Software");
            var testProduct = new Product(testName, testCategory, testPrice);

            testProduct.Name.Should().Be(testName);
            testProduct.Price.Should().Be(testPrice);
            testProduct.Category.Should().Be(testCategory);
            testProduct.CategoryId.Should().Be(testCategory.Id);
        }

        [Fact]
        public void Constructor_ShouldSetPriceAsZero_IfPriceIsNull()
        {
            var testName = "Visual Studio";
            decimal? testPrice = null;
            var testCategory = new Category("Software");
            var testProduct = new Product(testName, testCategory, testPrice);
            
            testProduct.Price.Should().Be(0);
        }

        [Fact]
        public void ChangeName_ShouldChangeName()
        {
            var testName = "Visual Studio";
            var testCategory = new Category("Software");
            var testProduct = new Product(testName, testCategory);
            var newName = "Word";

            testProduct.ChangeName(newName);
            testProduct.Name.Should().Be(newName);
        }

        [Fact]
        public void ChangeCategory_ShouldChangeCategory()
        {
            var testName = "Visual Studio";
            var testCategory = new Category("Software");
            var testProduct = new Product(testName, testCategory);
            var newCategory = new Category("Hardware");

            testProduct.ChangeCategory(newCategory);
            testProduct.Category.Should().Be(newCategory);
            testProduct.CategoryId.Should().Be(newCategory.Id);
        }

        [Fact]
        public void ChangePrice_ShouldChangePrice()
        {
            var testName = "Visual Studio";
            var testPrice = 10.85m;
            var testCategory = new Category("Software");
            var testProduct = new Product(testName, testCategory, testPrice);
            var newPrice = 0.99m;

            testProduct.ChangePrice(newPrice);
            testProduct.Price.Should().Be(newPrice);
        }
        
    }
}
