using FluentValidation;
using IntercambioGenebraAPI.Repositories;
using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Mediator;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using IntercambioGenebraAPI.ViewModels;

namespace IntercambioGenebraAPI.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Response>
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository repository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
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
