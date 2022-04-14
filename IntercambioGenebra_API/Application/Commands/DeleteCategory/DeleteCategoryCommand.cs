using IntercambioGenebraAPI.Application.Mediator;
using MediatR;

namespace IntercambioGenebraAPI.Application.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<Response>
    {
        public DeleteCategoryCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}