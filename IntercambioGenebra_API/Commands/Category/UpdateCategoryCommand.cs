using FluentValidation;

namespace IntercambioGenebraAPI.Commands.Category
{
    public class UpdateCategoryCommand
    {
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
