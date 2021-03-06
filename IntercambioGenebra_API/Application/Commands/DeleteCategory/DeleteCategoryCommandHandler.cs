using IntercambioGenebraAPI.Application.Mediator;
using IntercambioGenebraAPI.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Application.Commands.DeleteCategory
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

                var categoryHasAssociatedProducts = await _repository.CategoryHasAssociatedProducts(request.Id);
                if (categoryHasAssociatedProducts)
                {
                    response.Result =
                        new UnprocessableEntityObjectResult(
                            "Category could not be deleted because it has products associated with it.");
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