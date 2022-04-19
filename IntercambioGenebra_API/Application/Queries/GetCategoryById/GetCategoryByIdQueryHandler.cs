using IntercambioGenebraAPI.Application.Mediator;
using IntercambioGenebraAPI.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntercambioGenebraAPI.Application.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Response>
    {
        private readonly AppDbContext _context;

        public GetCategoryByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new Response();

            try
            {
                var category = await _context
                    .Categories
                    .FirstOrDefaultAsync(category => category.Id == request.Id);

                if (category == null)
                {
                    response.Result = new NotFoundObjectResult("Category not found.");
                    return response;
                }
                
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