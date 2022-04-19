using IntercambioGenebraAPI.Application.Mediator;
using IntercambioGenebraAPI.Domain.Entities;
using IntercambioGenebraAPI.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Application.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response>
    {
        private readonly ICategoryRepository _repository;

        public CreateCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateCategoryCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            var response = new Response();

            if (!validationResult.IsValid)
            {
                validationResult.Errors.ForEach(error => response.Errors.Add(error.ErrorMessage));
                response.Result = new UnprocessableEntityObjectResult(response.Errors);
                return response;
            }

            try
            {
                var category = new Category
                {
                    Name = request.Name
                };

                _repository.Insert(category);
                _repository.Save();
                response.Result = new OkObjectResult(category);
            }
            catch (Exception exception)
            {
                response.Errors.Add($"Error while creating category: {exception.Message}");
            }

            return response;
        }
    }
}