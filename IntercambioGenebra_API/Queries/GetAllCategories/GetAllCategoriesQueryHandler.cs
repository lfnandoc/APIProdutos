using FluentValidation;
using IntercambioGenebraAPI.Repositories;
using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Mediator;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Queries.GetAllCategories
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
                response.Result = new BadRequestObjectResult(exception.Message);
            }

            return response;
        }
    }
}
