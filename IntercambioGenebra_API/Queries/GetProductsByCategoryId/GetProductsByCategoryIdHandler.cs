using AutoMapper;
using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Mediator;
using IntercambioGenebraAPI.Repositories;
using IntercambioGenebraAPI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Queries.GetProductsByCategoryId
{
    public class GetProductsByCategoryIdQueryHandler : IRequestHandler<GetProductsByCategoryIdQuery, Response>
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetProductsByCategoryIdQueryHandler(IProductRepository repository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
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
                response.Result = new BadRequestObjectResult(exception.Message);
            }

            return response;
        }
    }
}
