using AutoMapper;
using GShop.ProductApi.Models;

namespace GShop.ProductApi.DTOs.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile() { 
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Product, ProductDTO>().ReverseMap();
    }
}
