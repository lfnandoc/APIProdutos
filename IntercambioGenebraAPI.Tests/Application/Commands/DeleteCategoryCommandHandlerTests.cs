using System;
using System.Threading;
using IntercambioGenebraAPI.Domain.Repositories;
using IntercambioGenebraAPI.Infrastructure;
using IntercambioGenebraAPI.Infrastructure.Repositories;
using IntercambioGenebraAPI.Tests.Utils.Factories;
using IntercambioGenebraAPI.Application.Commands.CreateCategory;
using Xunit;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using IntercambioGenebraAPI.Domain.Entities;
using IntercambioGenebraAPI.Application.Commands.DeleteCategory;

namespace IntercambioGenebraAPI.Tests.Application.Commands
{
    public class DeleteCategoryCommandHandlerTests
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly AppDbContext _context;

        public DeleteCategoryCommandHandlerTests()
        {
            _context = InMemoryContextFactory.Create();
            _categoryRepository = new CategoryRepository(_context);
            _productRepository = new ProductRepository(_context);
        }

        [Fact]
        public async Task Handle_ShouldDeleteCategory()
        {
            var category = CategoryFactory.CreateTestCategory(_categoryRepository);
            var categoryId = category.Id;

            var command = new DeleteCategoryCommand(categoryId);
            var handler = new DeleteCategoryCommandHandler(_categoryRepository);
            var response = await handler.Handle(command, CancellationToken.None);
            
            response.GetResult().Should().BeOfType<NoContentResult>();

            var deletedCategory = await _categoryRepository.GetCategoryByIdAsync(category.Id);
            
            deletedCategory.Should().BeNull();
        }

        [Fact]
        public async Task Handle_ShouldReturnUnprocessableEntity_IfCategoryHasProductsAssociatedWithIt()
        {
            var category = CategoryFactory.CreateTestCategoryWithProduct(_categoryRepository, _productRepository);
            var categoryId = category.Id;
            var command = new DeleteCategoryCommand(categoryId);
            var handler = new DeleteCategoryCommandHandler(_categoryRepository);
            var response = await handler.Handle(command, CancellationToken.None);
            
            response.GetResult().Should().BeOfType<UnprocessableEntityObjectResult>();
        }

    }
}
