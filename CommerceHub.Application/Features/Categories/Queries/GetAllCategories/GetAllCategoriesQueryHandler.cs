using AutoMapper;
using AutoMapper.QueryableExtensions;
using CommerceHub.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceHub.Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
    {
        private readonly ICommerceHubDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(ICommerceHubDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
