using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using IntercambioGenebraAPI.Application.Commands.UpdateCategory;
using IntercambioGenebraAPI.Application.Queries.GetCategoryById;
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
    public class GetCategoryByIdQueryHandlerTests
    {
        private readonly AppDbContext _context;

        public GetCategoryByIdQueryHandlerTests()
        {
            _context = InMemoryContextFactory.Create();
        }

        [Fact]
        public async Task Handle_ShouldReturnCategoryById()
        {
            var testCategory = CategoryFactory.CreateTestCategory(_context);
            var testCategoryId = testCategory.Id;
            var query = new GetCategoryByIdQuery(testCategoryId);
            var handler = new GetCategoryByIdQueryHandler(_context);
            var response = await handler.Handle(query, CancellationToken.None);
            var result = response?.GetResult().As<OkObjectResult>();
            var category = result?.Value.As<Category>();

            category.Should().NotBeNull();
            category!.Id.Should().Be(testCategoryId);
        }

        [Fact]
        public async Task Handle_ShouldReturnNotFound_IfCategoryDoesNotExist()
        {
            var testCategoryId = new Guid();
            var query = new GetCategoryByIdQuery(testCategoryId);
            var handler = new GetCategoryByIdQueryHandler(_context);
            var response = await handler.Handle(query, CancellationToken.None);
            
            response.GetResult().Should().BeOfType<NotFoundObjectResult>();
        }
    }
}
