using System;
using System.Threading;
using IntercambioGenebraAPI.Domain.Repositories;
using IntercambioGenebraAPI.Infrastructure;
using IntercambioGenebraAPI.Infrastructure.Repositories;
using IntercambioGenebraAPI.Tests.Utils.Factories;
using IntercambioGenebraAPI.Application.Commands.CreateProduct;
using Xunit;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using IntercambioGenebraAPI.Domain.Entities;
using IntercambioGenebraAPI.Application.Commands.DeleteProduct;
using IntercambioGenebraAPI.Tests.Utils.Factories.Entities;

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
