using FluentValidation;
using IntercambioGenebraAPI.Application.Mediator;
using MediatR;

namespace IntercambioGenebraAPI.Application.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<Response>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty()
                .WithMessage("Name is required.");
        }
    }
}