using AutoMapper;
using MangoRestaurant.Services.ProductApi.AutoMapper;

namespace MangoRestaurant.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductProfile());
            });
        }
    }
}
