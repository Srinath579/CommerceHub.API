using AutoMapper;
using AutoMapper.QueryableExtensions;
using CommerceHub.Application.Common;
using CommerceHub.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommerceHub.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PaginatedList<ProductDto>>
    {
        private readonly ICommerceHubDbContext _context;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(ICommerceHubDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Products.AsNoTracking();

            // 1. Apply category filtering if CategoryId is provided
            if (request.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == request.CategoryId.Value);
            }

            // 2. Apply search filtering if SearchTerm is provided
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(x => x.Name.Contains(request.SearchTerm) || x.Description.Contains(request.SearchTerm));
            }

            // 3. Order by name and apply AutoMapper ProjectTo optimization
            var projectedQuery = query
                .OrderBy(x => x.Name)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider);

            // 4. Execute paging via our helper
            return await PaginatedList<ProductDto>.CreateAsync(projectedQuery, request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
