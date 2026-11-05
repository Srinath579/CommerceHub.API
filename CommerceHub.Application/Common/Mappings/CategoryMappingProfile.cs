using AutoMapper;
using CommerceHub.Application.Features.Categories.Queries.GetAllCategories;
using CommerceHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommerceHub.Application.Common.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
