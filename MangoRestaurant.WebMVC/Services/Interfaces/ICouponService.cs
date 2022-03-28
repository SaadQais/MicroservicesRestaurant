namespace MangoRestaurant.WebMVC.Services.Interfaces
{
    public interface ICouponService
    {
        Task<T> GetByCodeAsync<T>(string couponCode, string token);
    }
}
