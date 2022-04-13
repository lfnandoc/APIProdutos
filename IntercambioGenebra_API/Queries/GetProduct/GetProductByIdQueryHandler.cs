using FluentValidation;
using IntercambioGenebraAPI.Repositories;
using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Mediator;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using IntercambioGenebraAPI.ViewModels;

namespace IntercambioGenebraAPI.Queries.GetProductById
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
                
                var productViewModel = new ProductViewModel()
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
