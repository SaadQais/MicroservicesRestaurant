using MangoRestaurant.Services.CouponApi.Models.Dto;

namespace MangoRestaurant.Services.CouponApi.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetByCodeAsync(string code);
    }
}
