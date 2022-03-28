using MangoRestaurant.WebMVC.Models;
using MangoRestaurant.WebMVC.Services.Interfaces;

namespace MangoRestaurant.WebMVC.Services
{
    public class CouponService : BaseService, ICouponService
    {
        private readonly IHttpClientFactory _httpClient;

        public CouponService(IHttpClientFactory httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetByCodeAsync<T>(string couponCode, string token)
        {
            return await SendAsync<T>(new ApiRequest
            {
                Method = SD.ApiMethod.GET,
                Url = SD.CouponAPIBase + $"/api/Coupons/GetCoupon/" + couponCode,
                AccessToken = token
            });
        }
    }
}
