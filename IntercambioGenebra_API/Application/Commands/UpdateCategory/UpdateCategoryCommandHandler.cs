using IntercambioGenebraAPI.Application.Mediator;
using IntercambioGenebraAPI.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Application.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Response>
    {
        private readonly ICategoryRepository _repository;

        public UpdateCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCategoryCommandValidator();
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
                var category = await _repository.GetCategoryByIdAsync(request.Id);

                if (category == null)
                {
                    response.Result = new NotFoundObjectResult("Category not found.");
                    return response;
                }

                category.Name = request.Name;
                _repository.Save();

                response.Result = new OkObjectResult(category);
            }
            catch (Exception exception)
            {
                response.Errors.Add($"Error while updating category: {exception.Message}");
            }

            return response;
        }
    }
}