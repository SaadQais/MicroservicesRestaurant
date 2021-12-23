using AutoMapper;
using MangoRestaurant.Services.ShoppingCartApi.Models;
using MangoRestaurant.Services.ShoppingCartApi.Models.Dto;

namespace MangoRestaurant.Services.ShoppingCartApi.AutoMapper
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
            CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
        }
    }
}
