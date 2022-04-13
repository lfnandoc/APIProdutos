using FluentValidation;
using IntercambioGenebraAPI.Repositories;
using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Mediator;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response>
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public CreateProductCommandHandler(IProductRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Response> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductCommandValidator();
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
                var category = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId);

                if (category == null)
                {
                    response.Result = new BadRequestObjectResult("Category not found.");
                    return response;
                }

                var product = new Product()
                {
                    Name = request.Name,
                    Price = request.Price ?? 0,
                    CategoryId = category.Id,
                    Category = category
                };

                _repository.Insert(product);
                _repository.Save();
                response.Result = new OkObjectResult(product);
            }
            catch (Exception exception)
            {
                response.Errors.Add($"Error while creating product: {exception.Message}");
            }

            return response;
        }
    }
}
