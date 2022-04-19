using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using IntercambioGenebraAPI.Application.Commands.UpdateCategory;
using IntercambioGenebraAPI.Domain.Entities;
using IntercambioGenebraAPI.Domain.Repositories;
using IntercambioGenebraAPI.Infrastructure;
using IntercambioGenebraAPI.Infrastructure.Repositories;
using IntercambioGenebraAPI.Tests.Utils.Factories;
using IntercambioGenebraAPI.Tests.Utils.Factories.Entities;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace IntercambioGenebraAPI.Tests.Application.Commands
{
    public class UpdateCategoryCommandHandlerTests
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly AppDbContext _context;

        public UpdateCategoryCommandHandlerTests()
        {
            _context = InMemoryContextFactory.Create();
            _categoryRepository = new CategoryRepository(_context);
        }

        [Fact]
        public async Task Handle_ShouldUpdateCategoryName()
        {
            const string newCategoryName = "Hardware";
            var testCategory = CategoryFactory.CreateTestCategory(_context);
            var command = new UpdateCategoryCommand()
            {
                Id = testCategory.Id,
                Name = newCategoryName
            };
            var handler = new UpdateCategoryCommandHandler(_categoryRepository);
            await handler.Handle(command, CancellationToken.None);
            
            var categoryFromDatatabase = await _categoryRepository.GetCategoryByIdAsync(testCategory!.Id);

            categoryFromDatatabase.Should().NotBeNull();
            categoryFromDatatabase!.Name.Should().Be(newCategoryName);
        }

        [Fact]
        public async Task Handle_ShouldReturnUnprocessableEntity_IfNameIsEmpty()
        {
            const string newCategoryName = "";
            var testCategory = CategoryFactory.CreateTestCategory(_context);
            var command = new UpdateCategoryCommand()
            {
                Id = testCategory.Id,
                Name = newCategoryName
            };
            var handler = new UpdateCategoryCommandHandler(_categoryRepository);
            var response = await handler.Handle(command, CancellationToken.None);

            response.GetResult().Should().BeOfType<UnprocessableEntityObjectResult>();
        }

        [Fact]
        public async Task Handle_ShouldReturnNotFound_IfCategoryDoesNotExist()
        {
            var testCategoryId = new Guid();
            var command = new UpdateCategoryCommand()
            {
                Id = testCategoryId,
                Name = "Hardware"
            };
            var handler = new UpdateCategoryCommandHandler(_categoryRepository);
            var response = await handler.Handle(command, CancellationToken.None);

            response.GetResult().Should().BeOfType<NotFoundObjectResult>();
        }
    }
}
