using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using IntercambioGenebraAPI.Application.Commands.UpdateCategory;
using IntercambioGenebraAPI.Application.Queries.GetAllCategories;
using IntercambioGenebraAPI.Domain.Entities;
using IntercambioGenebraAPI.Domain.Repositories;
using IntercambioGenebraAPI.Infrastructure;
using IntercambioGenebraAPI.Infrastructure.Repositories;
using IntercambioGenebraAPI.Tests.Utils.Factories;
using IntercambioGenebraAPI.Tests.Utils.Factories.Entities;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace IntercambioGenebraAPI.Tests.Application.Queries
{
    public class GetAllCategoriesQueryHandlerTests
    {
        private readonly AppDbContext _context;

        public GetAllCategoriesQueryHandlerTests()
        {
            _context = InMemoryContextFactory.Create();
        }

        [Fact]
        public async Task Handle_ShouldReturnAllCategoriesRegistered()
        {
            CategoryFactory.CreateTestCategory(_context, "Category1");
            CategoryFactory.CreateTestCategory(_context, "Category2");
            CategoryFactory.CreateTestCategory(_context, "Category3");

            var query = new GetAllCategoriesQuery();
            var handler = new GetAllCategoriesQueryHandler(_context);
            var response = await handler.Handle(query, CancellationToken.None);
            var result = response?.GetResult().As<OkObjectResult>();
            var categories = result?.Value.As<List<Category>>();

            categories.Should().NotBeNull().And.HaveCount(3);
        }

        [Fact]
        public async Task Handle_ShouldReturnNoCategories_IfNoCategoriesAreRegistered()
        { 
            var query = new GetAllCategoriesQuery();
            var handler = new GetAllCategoriesQueryHandler(_context);
            var response = await handler.Handle(query, CancellationToken.None);
            var result = response?.GetResult().As<OkObjectResult>();
            var categories = result?.Value.As<List<Category>>();

            categories.Should().NotBeNull().And.HaveCount(0);
        }
    }
}
