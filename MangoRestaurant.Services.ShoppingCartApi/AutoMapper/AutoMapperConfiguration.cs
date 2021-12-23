using AutoMapper;

namespace MangoRestaurant.Services.ShoppingCartApi.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ShoppingCartProfile());
            });
        }
    }
}
