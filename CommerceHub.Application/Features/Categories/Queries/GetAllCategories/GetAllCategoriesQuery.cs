using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommerceHub.Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<List<CategoryDto>>
    {
    }
}
