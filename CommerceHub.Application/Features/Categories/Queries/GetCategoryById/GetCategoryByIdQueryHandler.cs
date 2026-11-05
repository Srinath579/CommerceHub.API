using AutoMapper;
using AutoMapper.QueryableExtensions;
using CommerceHub.Application.Common;
using CommerceHub.Application.Features.Categories.Queries.GetAllCategories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommerceHub.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ICommerceHubDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(ICommerceHubDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                        .AsNoTracking()
                        .Where(x => x.Id == request.Id)
                        .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync(cancellationToken);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID '{request.Id}' was not found.");
            }

            return category;
        }
    }
}
