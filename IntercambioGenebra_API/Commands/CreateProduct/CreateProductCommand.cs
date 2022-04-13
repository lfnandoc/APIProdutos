using FluentValidation;
using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Mediator;
using MediatR;

namespace IntercambioGenebraAPI.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Response>
    {
        public string Name { get; set; }        
        public decimal? Price { get; set; }
        public Guid CategoryId { get; set; }
    }

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty()
                .WithMessage("Name is required.");

            RuleFor(command => command.CategoryId)
                .NotEmpty()
                .WithMessage("Product is required.");

            When(command => command.Price != null, () => {
                RuleFor(command => command.Price)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Price must be greater than or equal 0.");
            });            
        }
    }    
    
}
