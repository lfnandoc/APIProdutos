using IntercambioGenebraAPI.Application.Mediator;
using IntercambioGenebraAPI.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Application.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response>
    {
        private readonly IProductRepository _repository;

        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var response = new Response();

            try
            {
                var product = await _repository.GetProductByIdAsync(request.Id);

                if (product == null)
                {
                    response.Result = new NotFoundObjectResult("Product not found.");
                    return response;
                }

                _repository.Delete(product);
                _repository.Save();

                response.Result = new NoContentResult();
            }
            catch (Exception exception)
            {
                response.Errors.Add($"Error while deleting product: {exception.Message}");
            }

            return response;
        }
    }
}