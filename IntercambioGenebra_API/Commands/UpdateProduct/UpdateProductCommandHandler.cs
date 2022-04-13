using AutoMapper;
using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Mediator;
using IntercambioGenebraAPI.Repositories;
using IntercambioGenebraAPI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response>
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository repository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            var response = new Response();

            if (!validationResult.IsValid)
            {
                validationResult.Errors.ForEach(error => response.Errors.Add(error.ErrorMessage));
                response.Result = new BadRequestObjectResult(response.Errors);
                return response;
            }

            try
            {
                var product = await _repository.GetProductByIdAsync(request.Id);

                if (product == null)
                {
                    response.Result = new NotFoundObjectResult("Product not found.");
                    return response;
                }

                if (request.CategoryId != null)
                {
                    var category = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId.Value);

                    if (category == null)
                    {
                        response.Result = new BadRequestObjectResult("Category not found.");
                        return response;
                    }

                    product.CategoryId = category.Id;
                    product.Category = category;
                }

                if (request.Name != null)
                {
                    product.Name = request.Name;
                }

                if (request.Price != null)
                {
                    product.Price = (decimal)request.Price;
                }

                _repository.Save();
                var productViewModel = _mapper.Map<Product, ProductViewModel>(product);
                response.Result = new OkObjectResult(productViewModel);
            }
            catch (Exception exception)
            {
                response.Errors.Add($"Error while updating product: {exception.Message}");
            }

            return response;
        }
    }
}
