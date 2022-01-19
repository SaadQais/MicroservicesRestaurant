using AutoMapper;
using MangoRestaurant.Services.CouponApi.Models;
using MangoRestaurant.Services.CouponApi.Models.Dto;

namespace MangoRestaurant.Services.CouponApi.AutoMapper
{
    public class CouponProfile : Profile
    {
        public CouponProfile()
        {
            CreateMap<Coupon, CouponDto>().ReverseMap();
        }
    }
}
