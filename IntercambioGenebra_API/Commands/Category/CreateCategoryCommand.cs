using FluentValidation;
using IntercambioGenebraAPI.Mediator;
using MediatR;

namespace IntercambioGenebraAPI.Commands.Category
{
    public class CreateCategoryCommand : IRequest<Response<Entities.Category>>
    {
        public string Name { get; set; }
    }

    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty()
                .WithMessage("Name is required.");
        }
    }    
    
}
