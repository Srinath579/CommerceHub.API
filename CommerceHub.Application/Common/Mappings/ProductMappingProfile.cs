using AutoMapper;
using CommerceHub.Application.Features.Products.Queries.GetAllProducts;
using CommerceHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommerceHub.Application.Common.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // This tells AutoMapper: I want to map FROM Product TO ProductDto
            CreateMap<Product, ProductDto>();
        }
    }
}
