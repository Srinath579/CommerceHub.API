using AutoMapper;
using CommerceHub.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommerceHub.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly ICommerceHubDbContext _context;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(ICommerceHubDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            // 1. Fetch from database (AsNoTracking is faster for read-only queries)
            var products = await _context.Products
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            // 2. Map the List<Product> to a List<ProductDto>
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
