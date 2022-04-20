using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using IntercambioGenebraAPI.Application.Queries.GetAllProducts;
using IntercambioGenebraAPI.Domain.MapperProfiles;
using IntercambioGenebraAPI.Domain.ViewModels;
using IntercambioGenebraAPI.Infrastructure;
using IntercambioGenebraAPI.Tests.Utils.Factories;
using IntercambioGenebraAPI.Tests.Utils.Factories.Entities;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace IntercambioGenebraAPI.Tests.Application.Queries
{
    public class GetAllProductsQueryHandlerTests
    {
        private static MapperConfiguration ConfigMapper() => new MapperConfiguration(config => { config.AddProfile<ProductViewModelProfile>(); });
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public GetAllProductsQueryHandlerTests()
        {
            _context = InMemoryContextFactory.Create();
            _mapper = ConfigMapper().CreateMapper();
        }

        [Fact]
        public async Task Handle_ShouldReturnAllProductsRegistered()
        {
            ProductFactory.CreateTestProduct(_context, "Product1");
            ProductFactory.CreateTestProduct(_context, "Product2");
            ProductFactory.CreateTestProduct(_context, "Product3");

            var query = new GetAllProductsQuery();
            var handler = new GetAllProductsQueryHandler(_context, _mapper);
            var response = await handler.Handle(query, CancellationToken.None);
            var result = response?.GetResult().As<OkObjectResult>();
            var products = result?.Value.As<List<ProductViewModel>>();

            products.Should().NotBeNull().And.HaveCount(3);
        }

        [Fact]
        public async Task Handle_ShouldReturnNoProducts_IfNoProductsAreRegistered()
        { 
            var query = new GetAllProductsQuery();
            var handler = new GetAllProductsQueryHandler(_context, _mapper);
            var response = await handler.Handle(query, CancellationToken.None);
            var result = response?.GetResult().As<OkObjectResult>();
            var products = result?.Value.As<List<ProductViewModel>>();

            products.Should().NotBeNull().And.HaveCount(0);
        }
    }
}
