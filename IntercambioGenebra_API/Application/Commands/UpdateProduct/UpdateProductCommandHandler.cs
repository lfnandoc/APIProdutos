using AutoMapper;
using IntercambioGenebraAPI.Application.Mediator;
using IntercambioGenebraAPI.Domain.Entities;
using IntercambioGenebraAPI.Domain.Repositories;
using IntercambioGenebraAPI.Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Application.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandler(IProductRepository repository, ICategoryRepository categoryRepository,
            IMapper mapper)
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
                        response.Errors.Add("Category not found.");
                        return response;
                    }

                    product.ChangeCategory(category);
                }

                if (request.Name != null)
                    product.ChangeName(request.Name);
                

                if (request.Price != null)
                    product.ChangePrice(request.Price);

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