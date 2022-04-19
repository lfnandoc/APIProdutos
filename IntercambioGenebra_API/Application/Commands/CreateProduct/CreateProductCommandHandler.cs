using AutoMapper;
using IntercambioGenebraAPI.Application.Mediator;
using IntercambioGenebraAPI.Domain.Entities;
using IntercambioGenebraAPI.Domain.Repositories;
using IntercambioGenebraAPI.Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Application.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public CreateProductCommandHandler(IProductRepository repository, ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            var response = new Response();

            if (!validationResult.IsValid)
            {
                validationResult.Errors.ForEach(error => response.Errors.Add(error.ErrorMessage));
                return response;
            }

            try
            {
                var category = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId);

                if (category == null)
                {
                    response.Errors.Add("Category not found.");
                    return response;
                }

                var product = new Product
                {
                    Name = request.Name,
                    Price = request.Price ?? 0,
                    CategoryId = category.Id,
                    Category = category
                };

                _repository.Insert(product);
                _repository.Save();
                var productViewModel = _mapper.Map<Product, ProductViewModel>(product);
                response.Result = new OkObjectResult(productViewModel);
            }
            catch (Exception exception)
            {
                response.Errors.Add($"Error while creating product: {exception.Message}");
            }

            return response;
        }
    }
}