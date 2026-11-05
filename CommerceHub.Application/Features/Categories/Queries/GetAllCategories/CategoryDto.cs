using System;
using System.Collections.Generic;
using System.Text;

namespace CommerceHub.Application.Features.Categories.Queries.GetAllCategories
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
