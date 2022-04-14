using AutoMapper;
using IntercambioGenebraAPI.Application.Mediator;
using IntercambioGenebraAPI.Domain.Entities;
using IntercambioGenebraAPI.Domain.Repositories;
using IntercambioGenebraAPI.Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Application.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Response>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public GetAllProductsQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var response = new Response();

            try
            {
                var products = await _repository.GetAllProductsAsync();
                var productsViewModel = _mapper.Map<List<Product>, List<ProductViewModel>>(products);

                response.Result = new OkObjectResult(productsViewModel);
            }
            catch (Exception exception)
            {
                response.Result = new BadRequestObjectResult(exception.Message);
            }

            return response;
        }
    }
}