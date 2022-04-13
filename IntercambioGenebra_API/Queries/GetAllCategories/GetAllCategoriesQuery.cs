using FluentValidation;
using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Mediator;
using MediatR;

namespace IntercambioGenebraAPI.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<Response>
    {
    }
}
