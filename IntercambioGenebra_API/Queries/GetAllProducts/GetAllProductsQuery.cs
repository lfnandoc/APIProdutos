﻿using FluentValidation;
using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Mediator;
using MediatR;

namespace IntercambioGenebraAPI.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<Response>
    {
    }
}