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

namespace IntercambioGenebraAPI.Tests.Application.Commands
{
    public class CreateCategoryCommandHandlerTests
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly AppDbContext _context;

        public CreateCategoryCommandHandlerTests()
        {
            _context = InMemoryContextFactory.Create();
            _categoryRepository = new CategoryRepository(_context);
        }

        [Fact]
        public async Task Handle_ShouldCreateCategory()
        {
            const string testCategoryName = "Software";
            var command = new CreateCategoryCommand() { Name = testCategoryName };
            var handler = new CreateCategoryCommandHandler(_categoryRepository);
            var response = await handler.Handle(command, new CancellationToken());
            var result = response?.GetResult().As<OkObjectResult>();
            var category = result?.Value.As<Category>();

            category.Should().NotBeNull();

            var categoryFromDatatabase = await _categoryRepository.GetCategoryByIdAsync(category!.Id);
            
            categoryFromDatatabase.Should().NotBeNull();
            categoryFromDatatabase!.Name.Should().Be(testCategoryName);
        }

        [Fact]
        public async Task Handle_ShouldReturnUnprocessableEntity_IfNameIsEmpty()
        {
            const string testCategoryName = "";
            var command = new CreateCategoryCommand() { Name = testCategoryName };
            var handler = new CreateCategoryCommandHandler(_categoryRepository);
            var response = await handler.Handle(command, new CancellationToken());
            
            response.GetResult().Should().BeOfType<UnprocessableEntityObjectResult>();
        }
    }
}