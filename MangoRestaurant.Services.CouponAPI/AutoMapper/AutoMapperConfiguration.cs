using AutoMapper;
using MangoRestaurant.Services.CouponApi.AutoMapper;

namespace MangoRestaurant.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CouponProfile());
            });
        }
    }
}
