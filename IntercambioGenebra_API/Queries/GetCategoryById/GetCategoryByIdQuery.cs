using FluentValidation;
using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Mediator;
using MediatR;

namespace IntercambioGenebraAPI.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<Response>
    {
        public Guid Id { get; set; }
    }
}
