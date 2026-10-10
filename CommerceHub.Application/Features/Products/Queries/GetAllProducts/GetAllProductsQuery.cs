using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommerceHub.Application.Features.Products.Queries.GetAllProducts
{
    // The query returns a List of ProductDto
    public class GetAllProductsQuery : IRequest<List<ProductDto>>
    {
    }
}
