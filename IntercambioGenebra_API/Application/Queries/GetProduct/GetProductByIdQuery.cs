using IntercambioGenebraAPI.Application.Mediator;
using MediatR;

namespace IntercambioGenebraAPI.Application.Queries.GetProduct
{
    public class GetProductByIdQuery : IRequest<Response>
    {
        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}