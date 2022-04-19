using IntercambioGenebraAPI.Application.Mediator;
using IntercambioGenebraAPI.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntercambioGenebraAPI.Application.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Response>
    {
        private readonly AppDbContext _context;

        public GetAllCategoriesQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var response = new Response();

            try
            {
                var category = await _context
                    .Categories
                    .AsNoTracking()
                    .ToListAsync();

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