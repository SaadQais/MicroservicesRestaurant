using AutoMapper;
using MangoRestaurant.Services.ProductApi.Models;
using MangoRestaurant.Services.ProductApi.Models.Dto;

namespace MangoRestaurant.Services.ProductApi.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
