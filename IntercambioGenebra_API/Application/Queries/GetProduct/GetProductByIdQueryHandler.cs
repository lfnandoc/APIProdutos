using IntercambioGenebraAPI.Application.Mediator;
using IntercambioGenebraAPI.Domain.Repositories;
using IntercambioGenebraAPI.Domain.ViewModels;
using IntercambioGenebraAPI.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntercambioGenebraAPI.Application.Queries.GetProduct
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Response>
    {
        private readonly AppDbContext _context;

        public GetProductByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new Response();

            try
            {
                var product = await _context
                    .Products
                    .Include(product => product.Category)
                    .FirstOrDefaultAsync(product => product.Id == request.Id);

                if (product == null)
                {
                    response.Result = new NotFoundObjectResult("Product not found.");
                    return response;
                }

                var productViewModel = new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    CategoryName = product.Category.Name
                };

                response.Result = new OkObjectResult(productViewModel);
            }
            catch (Exception exception)
            {
                response.Result = new UnprocessableEntityObjectResult(exception.Message);
            }

            return response;
        }
    }
}