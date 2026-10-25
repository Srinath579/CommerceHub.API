using CommerceHub.Application.Features.Products.Queries.GetAllProducts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommerceHub.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public Guid Id { get; set; }
    }
}
