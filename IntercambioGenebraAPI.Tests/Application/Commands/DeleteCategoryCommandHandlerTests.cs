using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using IntercambioGenebraAPI.Application.Commands.DeleteCategory;
using IntercambioGenebraAPI.Domain.Repositories;
using IntercambioGenebraAPI.Infrastructure;
using IntercambioGenebraAPI.Infrastructure.Repositories;
using IntercambioGenebraAPI.Tests.Utils.Factories;
using IntercambioGenebraAPI.Tests.Utils.Factories.Entities;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace IntercambioGenebraAPI.Tests.Application.Commands
{
    public class DeleteCategoryCommandHandlerTests
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly AppDbContext _context;

        public DeleteCategoryCommandHandlerTests()
        {
            _context = InMemoryContextFactory.Create();
            _categoryRepository = new CategoryRepository(_context);
        }

        [Fact]
        public async Task Handle_ShouldDeleteCategory()
        {
            var category = CategoryFactory.CreateTestCategory(_context);
            var categoryId = category.Id;
            var command = new DeleteCategoryCommand(categoryId);
            var handler = new DeleteCategoryCommandHandler(_categoryRepository);
            var response = await handler.Handle(command, CancellationToken.None);
            
            response.GetResult().Should().BeOfType<NoContentResult>();

            var deletedCategory = await _categoryRepository.GetCategoryByIdAsync(category.Id);
            
            deletedCategory.Should().BeNull();
        }

        [Fact]
        public async Task Handle_ShouldReturnNotFound_IfCategoryDoesNotExist()
        {
            var categoryId = new Guid();
            var command = new DeleteCategoryCommand(categoryId);
            var handler = new DeleteCategoryCommandHandler(_categoryRepository);
            var response = await handler.Handle(command, CancellationToken.None);

            response.GetResult().Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Handle_ShouldReturnUnprocessableEntity_IfCategoryHasProductsAssociatedWithIt()
        {
            var category = CategoryFactory.CreateTestCategoryWithProduct(_context);
            var categoryId = category.Id;
            var command = new DeleteCategoryCommand(categoryId);
            var handler = new DeleteCategoryCommandHandler(_categoryRepository);
            var response = await handler.Handle(command, CancellationToken.None);
            
            response.GetResult().Should().BeOfType<UnprocessableEntityObjectResult>();
        }

    }
}
