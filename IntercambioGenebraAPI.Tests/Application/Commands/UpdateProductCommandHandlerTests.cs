using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using IntercambioGenebraAPI.Application.Commands.UpdateProduct;
using IntercambioGenebraAPI.Domain.Entities;
using IntercambioGenebraAPI.Domain.MapperProfiles;
using IntercambioGenebraAPI.Domain.Repositories;
using IntercambioGenebraAPI.Infrastructure;
using IntercambioGenebraAPI.Infrastructure.Repositories;
using IntercambioGenebraAPI.Tests.Utils.Factories;
using IntercambioGenebraAPI.Tests.Utils.Factories.Entities;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace IntercambioGenebraAPI.Tests.Application.Commands
{
    public class UpdateProductCommandHandlerTests
    {
        private static MapperConfiguration ConfigMapper() => new MapperConfiguration(config => { config.AddProfile<ProductViewModelProfile>(); });
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly AppDbContext _context;

        public UpdateProductCommandHandlerTests()
        {
            _context = InMemoryContextFactory.Create();
            _productRepository = new ProductRepository(_context);
            _categoryRepository = new CategoryRepository(_context);
            _mapper = ConfigMapper().CreateMapper();
        }

        [Fact]
        public async Task Handle_ShouldUpdateProductName()
        {
            const string newProductName = "Word";
            var testProduct = ProductFactory.CreateTestProduct(_context);
            var command = new UpdateProductCommand()
            {
                Id = testProduct.Id,
                Name = newProductName
            };
            var handler = new UpdateProductCommandHandler(_productRepository, _categoryRepository, _mapper);
            await handler.Handle(command, CancellationToken.None);
            
            var productFromDatabase = await _productRepository.GetProductByIdAsync(testProduct!.Id);

            productFromDatabase.Should().NotBeNull();
            productFromDatabase!.Name.Should().Be(newProductName);
        }

        [Fact]
        public async Task Handle_ShouldReturnUnprocessableEntity_IfNameIsEmpty()
        {
            const string newProductName = "";
            var testProduct = ProductFactory.CreateTestProduct(_context);
            var command = new UpdateProductCommand()
            {
                Id = testProduct.Id,
                Name = newProductName
            };
            var handler = new UpdateProductCommandHandler(_productRepository, _categoryRepository, _mapper);
            var response = await handler.Handle(command, CancellationToken.None);

            response.GetResult().Should().BeOfType<UnprocessableEntityObjectResult>();
        }

        [Fact]
        public async Task Handle_ShouldUpdateCategory()
        {           
            var testProduct = ProductFactory.CreateTestProduct(_context);
            var newCategory = CategoryFactory.CreateTestCategory(_context);
            var newCategoryId = newCategory.Id;
            var command = new UpdateProductCommand()
            {
                Id = testProduct.Id,
                CategoryId = newCategoryId
            };
            var handler = new UpdateProductCommandHandler(_productRepository, _categoryRepository, _mapper);
            await handler.Handle(command, CancellationToken.None);

            var productFromDatabase = await _productRepository.GetProductByIdAsync(testProduct!.Id);

            productFromDatabase.Should().NotBeNull();
            productFromDatabase!.CategoryId.Should().Be(newCategoryId);
        }

        [Fact]
        public async Task Handle_ShouldReturnUnprocessableEntity_IfCategoryDoesNotExist()
        {
            var testProduct = ProductFactory.CreateTestProduct(_context);
            var command = new UpdateProductCommand()
            {
                Id = testProduct.Id,
                CategoryId = new Guid()
            };
            var handler = new UpdateProductCommandHandler(_productRepository, _categoryRepository, _mapper);
            var response = await handler.Handle(command, CancellationToken.None);

            response.GetResult().Should().BeOfType<UnprocessableEntityObjectResult>();
        }

        [Fact]
        public async Task Handle_ShouldUpdatePrice()
        {
            const decimal newProductPrice = 1.99m;
            var testProduct = ProductFactory.CreateTestProduct(_context);
            var command = new UpdateProductCommand()
            {
                Id = testProduct.Id,
                Price = newProductPrice
            };
            var handler = new UpdateProductCommandHandler(_productRepository, _categoryRepository, _mapper);
            await handler.Handle(command, CancellationToken.None);

            var productFromDatabase = await _productRepository.GetProductByIdAsync(testProduct!.Id);

            productFromDatabase.Should().NotBeNull();
            productFromDatabase!.Price.Should().Be(newProductPrice);
        }

        [Fact]
        public async Task Handle_ShouldReturnUnprocessableEntity_IfPriceIsNegative()
        {
            const decimal newProductPrice = -5m;
            var testProduct = ProductFactory.CreateTestProduct(_context);
            var command = new UpdateProductCommand()
            {
                Id = testProduct.Id,
                Price = newProductPrice
            };
            var handler = new UpdateProductCommandHandler(_productRepository, _categoryRepository, _mapper);
            var response = await handler.Handle(command, CancellationToken.None);

            response.GetResult().Should().BeOfType<UnprocessableEntityObjectResult>();
        }

    }
}
