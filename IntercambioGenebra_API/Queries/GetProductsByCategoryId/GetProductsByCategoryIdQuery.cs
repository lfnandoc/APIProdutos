using IntercambioGenebraAPI.Mediator;
using MediatR;

namespace IntercambioGenebraAPI.Queries.GetProductsByCategoryId
{
    public class GetProductsByCategoryIdQuery : IRequest<Response>
    {
        public Guid CategoryId { get; set; }

        public GetProductsByCategoryIdQuery(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
