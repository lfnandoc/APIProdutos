using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using IntercambioGenebraAPI.Application.Commands.CreateProduct;
using IntercambioGenebraAPI.Domain.MapperProfiles;
using IntercambioGenebraAPI.Domain.Repositories;
using IntercambioGenebraAPI.Domain.ViewModels;
using IntercambioGenebraAPI.Infrastructure;
using IntercambioGenebraAPI.Infrastructure.Repositories;
using IntercambioGenebraAPI.Tests.Utils.Factories;
using IntercambioGenebraAPI.Tests.Utils.Factories.Entities;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace IntercambioGenebraAPI.Tests.Application.Commands
{
    public class CreateProductCommandHandlerTests
    {
        private static MapperConfiguration ConfigMapper() => new MapperConfiguration(config => { config.AddProfile<ProductViewModelProfile>(); });
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CreateProductCommandHandlerTests()
        {
            _context = InMemoryContextFactory.Create();
            _productRepository = new ProductRepository(_context);
            _categoryRepository = new CategoryRepository(_context);
            _mapper = ConfigMapper().CreateMapper();
        }

        [Fact]
        public async Task Handle_ShouldCreateProduct()
        {
            const string testProductName = "Visual Studio";
            const decimal testProductPrice = 10.85m;
            var category = CategoryFactory.CreateTestCategory(_context);

            var command = new CreateProductCommand() 
            { 
                Name = testProductName, 
                Price = testProductPrice, 
                CategoryId = category.Id
            };
            
            var handler = new CreateProductCommandHandler(_productRepository, _categoryRepository, _mapper);
            var response = await handler.Handle(command, new CancellationToken());
            var result = response?.GetResult().As<OkObjectResult>();
            var product = result?.Value.As<ProductViewModel>();

            product.Should().NotBeNull();

            var productFromDatatabase = await _productRepository.GetProductByIdAsync(product!.Id);

            productFromDatatabase.Should().NotBeNull();
            productFromDatatabase!.Name.Should().Be(testProductName);
            productFromDatatabase!.Price.Should().Be(testProductPrice);
            productFromDatatabase!.CategoryId.Should().Be(category.Id);
        }

        [Fact]
        public async Task Handle_ShouldCreateProductWithPriceZero_IfPriceNotProvided()
        {
            const string testProductName = "Visual Studio";
            var category = CategoryFactory.CreateTestCategory(_context);

            var command = new CreateProductCommand()
            {
                Name = testProductName,
                CategoryId = category.Id
            };

            var handler = new CreateProductCommandHandler(_productRepository, _categoryRepository, _mapper);
            var response = await handler.Handle(command, new CancellationToken());
            var result = response?.GetResult().As<OkObjectResult>();
            var product = result?.Value.As<ProductViewModel>();

            product.Should().NotBeNull();

            var productFromDatatabase = await _productRepository.GetProductByIdAsync(product!.Id);

            productFromDatatabase.Should().NotBeNull();
            productFromDatatabase!.Price.Should().Be(0);
        }

        [Fact]
        public async Task Handle_ShouldReturnUnprocessableEntity_IfNameIsEmpty()
        {
            const string testProductName = "";
            const decimal testProductPrice = 10.85m;
            var category = CategoryFactory.CreateTestCategory(_context);

            var command = new CreateProductCommand()
            {
                Name = testProductName,
                Price = testProductPrice,
                CategoryId = category.Id
            };

            var handler = new CreateProductCommandHandler(_productRepository, _categoryRepository, _mapper);
            var response = await handler.Handle(command, new CancellationToken());

            response.GetResult().Should().BeOfType<UnprocessableEntityObjectResult>();
        }

        [Fact]
        public async Task Handle_ShouldReturnUnprocessableEntity_IfCategoryDoesNotExist()
        {
            const string testProductName = "Visual Studio";
            const decimal testProductPrice = 10.85m;

            var command = new CreateProductCommand()
            {
                Name = testProductName,
                Price = testProductPrice,
                CategoryId = new Guid()
            };

            var handler = new CreateProductCommandHandler(_productRepository, _categoryRepository, _mapper);
            var response = await handler.Handle(command, new CancellationToken());

            response.GetResult().Should().BeOfType<UnprocessableEntityObjectResult>();
        }

        [Fact]
        public async Task Handle_ShouldReturnUnprocessableEntity_IfPriceIsNegative()
        {
            const string testProductName = "Visual Studio";
            const decimal testProductPrice = -5m;
            var category = CategoryFactory.CreateTestCategory(_context);

            var command = new CreateProductCommand()
            {
                Name = testProductName,
                Price = testProductPrice,
                CategoryId = category.Id
            };

            var handler = new CreateProductCommandHandler(_productRepository, _categoryRepository, _mapper);
            var response = await handler.Handle(command, new CancellationToken());

            response.GetResult().Should().BeOfType<UnprocessableEntityObjectResult>();
        }

        [Fact]
        public async Task Handle_ShouldReturnUnprocessableEntity_IfNoCategoryIsProvided()
        {
            const string testProductName = "Visual Studio";
            const decimal testProductPrice = 10.85m;

            var command = new CreateProductCommand()
            {
                Name = testProductName,
                Price = testProductPrice,
                CategoryId = Guid.Empty
            };

            var handler = new CreateProductCommandHandler(_productRepository, _categoryRepository, _mapper);
            var response = await handler.Handle(command, new CancellationToken());

            response.GetResult().Should().BeOfType<UnprocessableEntityObjectResult>();
        }
    }
}