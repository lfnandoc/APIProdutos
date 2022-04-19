using Xunit;
using FluentAssertions;
using IntercambioGenebraAPI.Domain.Entities;

namespace IntercambioGenebraAPI.Tests.Domain.Entities
{
    public class CategoryTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            var testName = "Software";
            var testCategory = new Category(testName);

            testCategory.Name.Should().Be(testName);
        }

        [Fact]
        public void ChangeName_ShouldChangeName()
        {
            var testName = "Software";
            var testCategory = new Category(testName);
            var newName = "Hardware";

            testCategory.ChangeName(newName);
            testCategory.Name.Should().Be(newName);
        }
    }
}
