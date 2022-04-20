using AutoMapper;
using IntercambioGenebraAPI.Application.Mediator;
using IntercambioGenebraAPI.Domain.Entities;
using IntercambioGenebraAPI.Domain.ViewModels;
using IntercambioGenebraAPI.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntercambioGenebraAPI.Application.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Response>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public GetAllProductsQueryHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var response = new Response();

            try
            {
                var products = await _context
                    .Products
                    .Include(product => product.Category)
                    .AsNoTracking()
                    .ToListAsync();

                var productsViewModel = _mapper.Map<List<Product>, List<ProductViewModel>>(products);

                response.Result = new OkObjectResult(productsViewModel);
            }
            catch (Exception exception)
            {
                response.Result = new UnprocessableEntityObjectResult(exception.Message);
            }

            return response;
        }
    }
}