using IntercambioGenebraAPI.Mediator;
using IntercambioGenebraAPI.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Response>
    {
        private readonly ICategoryRepository _repository;

        public GetCategoryByIdQueryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new Response();

            try
            {
                var category = await _repository.GetCategoryByIdAsync(request.Id);
                response.Result = new OkObjectResult(category);
            }
            catch (Exception exception)
            {
                response.Result = new BadRequestObjectResult(exception.Message);
            }

            return response;
        }
    }
}
