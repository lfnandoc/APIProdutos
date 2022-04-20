using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using IntercambioGenebraAPI.Application.Commands.DeleteProduct;
using IntercambioGenebraAPI.Domain.Repositories;
using IntercambioGenebraAPI.Infrastructure;
using IntercambioGenebraAPI.Infrastructure.Repositories;
using IntercambioGenebraAPI.Tests.Utils.Factories;
using IntercambioGenebraAPI.Tests.Utils.Factories.Entities;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace IntercambioGenebraAPI.Tests.Application.Commands
{
    public class DeleteProductCommandHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly AppDbContext _context;

        public DeleteProductCommandHandlerTests()
        {
            _context = InMemoryContextFactory.Create();
            _productRepository = new ProductRepository(_context);
        }

        [Fact]
        public async Task Handle_ShouldDeleteProduct()
        {
            var product = ProductFactory.CreateTestProduct(_context);
            var productId = product.Id;
            var command = new DeleteProductCommand(productId);
            var handler = new DeleteProductCommandHandler(_productRepository);
            var response = await handler.Handle(command, CancellationToken.None);
            
            response.GetResult().Should().BeOfType<NoContentResult>();

            var deletedProduct = await _productRepository.GetProductByIdAsync(product.Id);
            
            deletedProduct.Should().BeNull();
        }

        [Fact]
        public async Task Handle_ShouldReturnNotFound_IfProductDoesNotExist()
        {
            var productId = new Guid();
            var command = new DeleteProductCommand(productId);
            var handler = new DeleteProductCommandHandler(_productRepository);
            var response = await handler.Handle(command, CancellationToken.None);

            response.GetResult().Should().BeOfType<NotFoundObjectResult>();
        }

    }
}
