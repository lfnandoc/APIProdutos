using IntercambioGenebraAPI.Application.Mediator;
using MediatR;

namespace IntercambioGenebraAPI.Application.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<Response>
    {
        public GetCategoryByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}