using FluentValidation;
using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Mediator;
using MediatR;

namespace IntercambioGenebraAPI.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<Response>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
