using AutoMapper;
using IntercambioGenebraAPI.Application.Mediator;
using IntercambioGenebraAPI.Domain.Entities;
using IntercambioGenebraAPI.Domain.Repositories;
using IntercambioGenebraAPI.Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Application.Queries.GetProductsByCategoryId
{
    public class GetProductsByCategoryIdQueryHandler : IRequestHandler<GetProductsByCategoryIdQuery, Response>
    {
        
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public GetProductsByCategoryIdQueryHandler(IProductRepository repository, 
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetProductsByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var response = new Response();

            try
            {
                var products = await _repository.GetProductsByCategoryIdAsync(request.CategoryId);
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