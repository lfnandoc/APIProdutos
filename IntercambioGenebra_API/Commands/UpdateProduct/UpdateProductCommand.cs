using FluentValidation;
using IntercambioGenebraAPI.Mediator;
using MediatR;

namespace IntercambioGenebraAPI.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Response>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public Guid? CategoryId { get; set; }
    }

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            When(command => command.Name != null, () =>
            {
                RuleFor(command => command.Name)
                .NotEmpty()
                .WithMessage("Name should not be empty.");
            });

            When(command => command.Price != null, () =>
            {
                RuleFor(command => command.Price)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Price must be greater than or equal 0.");
            });
        }
    }
}
