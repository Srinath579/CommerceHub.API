using AutoMapper;
using AutoMapper.QueryableExtensions;
using CommerceHub.Application.Common;
using CommerceHub.Application.Features.Products.Queries.GetAllProducts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommerceHub.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly ICommerceHubDbContext _context;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(ICommerceHubDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            // 1. Use ProjectTo to fetch and map only the required columns efficiently
            var product = await _context.Products
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            // 2. If null, throw KeyNotFoundException (GlobalExceptionHandler maps this to 404)
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID '{request.Id}' was not found.");
            }

            return product;
        }
    }
}
