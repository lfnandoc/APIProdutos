using FluentValidation;
using IntercambioGenebraAPI.Mediator;
using MediatR;

namespace IntercambioGenebraAPI.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<Response>
    {
        public Guid Id { get; set; }
    }
}
