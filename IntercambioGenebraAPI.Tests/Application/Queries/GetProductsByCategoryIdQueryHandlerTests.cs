using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using IntercambioGenebraAPI.Application.Commands.UpdateProduct;
using IntercambioGenebraAPI.Application.Queries.GetProductsByCategoryId;
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
    public class GetProductsByCategoryIdQueryHandlerTests
    {
        private static MapperConfiguration ConfigMapper() => new MapperConfiguration(config => { config.AddProfile<ProductViewModelProfile>(); });
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public GetProductsByCategoryIdQueryHandlerTests()
        {
            _context = InMemoryContextFactory.Create();
            _mapper = ConfigMapper().CreateMapper();
        }

        [Fact]
        public async Task Handle_ShouldReturnOnlyProductsOfProvidedCategoryId()
        {
            var testCategory = CategoryFactory.CreateTestCategory(_context);
            ProductFactory.CreateTestProductOfCategory(_context, testCategory, "Product1");
            ProductFactory.CreateTestProductOfCategory(_context, testCategory, "Product2");
            ProductFactory.CreateTestProductOfCategory(_context, testCategory, "Product3");
            ProductFactory.CreateTestProduct(_context, "Product4");
            ProductFactory.CreateTestProduct(_context, "Product5");

            var query = new GetProductsByCategoryIdQuery(testCategory.Id);
            var handler = new GetProductsByCategoryIdQueryHandler(_context, _mapper);
            var response = await handler.Handle(query, CancellationToken.None);
            var result = response?.GetResult().As<OkObjectResult>();
            var products = result?.Value.As<List<ProductViewModel>>();

            products.Should().NotBeNull().And.HaveCount(3);
        }

        [Fact]
        public async Task Handle_ShouldReturnNoProducts_IfNoProductsOfThatCategoryAreRegistered()
        { 
            var query = new GetProductsByCategoryIdQuery(new Guid());
            var handler = new GetProductsByCategoryIdQueryHandler(_context, _mapper);
            var response = await handler.Handle(query, CancellationToken.None);
            var result = response?.GetResult().As<OkObjectResult>();
            var products = result?.Value.As<List<ProductViewModel>>();

            products.Should().NotBeNull().And.HaveCount(0);
        }
    }
}
