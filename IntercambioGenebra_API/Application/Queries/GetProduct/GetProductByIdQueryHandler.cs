using IntercambioGenebraAPI.Application.Mediator;
using IntercambioGenebraAPI.Domain.Repositories;
using IntercambioGenebraAPI.Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Application.Queries.GetProduct
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Response>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new Response();

            try
            {
                var product = await _repository.GetProductByIdAsync(request.Id);

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
                response.Result = new BadRequestObjectResult(exception.Message);
            }

            return response;
        }
    }
}