using FluentValidation;
using IntercambioGenebraAPI.Repositories;
using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Mediator;
using MediatR;

namespace IntercambioGenebraAPI.Commands.Category
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<Entities.Category>>
    {
        private readonly ICategoryRepository _repository;

        public CreateCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<Entities.Category>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateCategoryCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            var response = new Response<Entities.Category>();

            if (!validationResult.IsValid)            
                validationResult.Errors.ForEach(error => response.Errors.Add(error.ErrorMessage));

            try
            {
                var category = new Entities.Category()
                {
                    Name = request.Name
                };

                _repository.Insert(category);
                _repository.Save();
                response.Result = category;
            }
            catch (Exception exception)
            {
                response.Errors.Add($"Error while creating category: {exception.Message}");
            }

            return response;
        }
    }
}
