using IntercambioGenebraAPI.Application.Mediator;
using MediatR;

namespace IntercambioGenebraAPI.Application.Queries.GetProductsByCategoryId
{
    public class GetProductsByCategoryIdQuery : IRequest<Response>
    {
        public GetProductsByCategoryIdQuery(Guid categoryId)
        {
            CategoryId = categoryId;
        }

        public Guid CategoryId { get; set; }
    }
}