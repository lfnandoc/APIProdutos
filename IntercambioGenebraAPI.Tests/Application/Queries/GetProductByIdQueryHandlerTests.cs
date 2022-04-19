using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using IntercambioGenebraAPI.Application.Commands.UpdateProduct;
using IntercambioGenebraAPI.Application.Queries.GetAllProducts;
using IntercambioGenebraAPI.Application.Queries.GetProduct;
using IntercambioGenebraAPI.Domain.Entities;
using IntercambioGenebraAPI.Domain.MapperProfiles;
using IntercambioGenebraAPI.Domain.Repositories;
using IntercambioGenebraAPI.Domain.ViewModels;
using IntercambioGenebraAPI.Infrastructure;
using IntercambioGenebraAPI.Infrastructure.Repositories;
using IntercambioGenebraAPI.Tests.Utils.Factories;
using IntercambioGenebraAPI.Tests.Utils.Factories.Entities;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace IntercambioGenebraAPI.Tests.Application.Queries
{
    public class GetProductByIdQueryHandlerTests
    {
        private readonly AppDbContext _context;

        public GetProductByIdQueryHandlerTests()
        {
            _context = InMemoryContextFactory.Create();
        }

        [Fact]
        public async Task Handle_ShouldReturnProductById()
        {
            var testProduct = ProductFactory.CreateTestProduct(_context);
            var testProductId = testProduct.Id;
            var query = new GetProductByIdQuery(testProductId);
            var handler = new GetProductByIdQueryHandler(_context);
            var response = await handler.Handle(query, CancellationToken.None);
            var result = response?.GetResult().As<OkObjectResult>();
            var product = result?.Value.As<ProductViewModel>();

            product.Should().NotBeNull();
            product!.Id.Should().Be(testProductId);
        }

        [Fact]
        public async Task Handle_ShouldReturnNotFound_IfProductDoesNotExist()
        {
            var testProductId = new Guid();
            var query = new GetProductByIdQuery(testProductId);
            var handler = new GetProductByIdQueryHandler(_context);
            var response = await handler.Handle(query, CancellationToken.None);
            
            response.GetResult().Should().BeOfType<NotFoundObjectResult>();
        }
    }
}
