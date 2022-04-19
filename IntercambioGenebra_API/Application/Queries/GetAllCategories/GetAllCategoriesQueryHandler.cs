using IntercambioGenebraAPI.Application.Mediator;
using IntercambioGenebraAPI.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Application.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Response>
    {
        private readonly ICategoryRepository _repository;

        public GetAllCategoriesQueryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var response = new Response();

            try
            {
                var category = await _repository.GetAllCategories();
                response.Result = new OkObjectResult(category);
            }
            catch (Exception exception)
            {
                response.Result = new UnprocessableEntityObjectResult(exception.Message);
            }

            return response;
        }
    }
}