using FluentValidation;
using IntercambioGenebraAPI.Repositories;
using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Mediator;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Response>
    {
        private readonly ICategoryRepository _repository;

        public DeleteCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {            
            var response = new Response();
            
            try
            {
                var category = await _repository.GetCategoryByIdAsync(request.Id);

                if (category == null)
                {
                    response.Result = new NotFoundObjectResult("Category not found.");
                    return response;
                }

                _repository.Delete(category);
                _repository.Save();

                response.Result = new NoContentResult();
            }
            catch (Exception exception)
            {
                response.Errors.Add($"Error while deleting category: {exception.Message}");
            }

            return response;
        }
    }
}
