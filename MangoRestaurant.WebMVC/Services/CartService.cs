using MangoRestaurant.WebMVC.Models;
using MangoRestaurant.WebMVC.Models.ShoppingCart;
using MangoRestaurant.WebMVC.Services.Interfaces;

namespace MangoRestaurant.WebMVC.Services
{
    public class CartService : BaseService, ICartService
    {
        private readonly IHttpClientFactory _httpClient;

        public CartService(IHttpClientFactory httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByUserIdAsync<T>(string userId, string token)
        {
            return await SendAsync<T>(new ApiRequest
            {
                Method = SD.ApiMethod.GET,
                Url = SD.ShoppingCartAPIBase + $"/api/cart/GetCart" + userId,
                AccessToken = token
            });
        }

        public async Task<T> CreateAsync<T>(CartDto cartDto, string token)
        {
            return await SendAsync<T>(new ApiRequest
            {
                Method = SD.ApiMethod.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/AddCart",
                AccessToken = token
            });
        }

        public async Task<T> UpdateAsync<T>(CartDto cartDto, string token)
        {
            return await SendAsync<T>(new ApiRequest
            {
                Method = SD.ApiMethod.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/UpdateCart",
                AccessToken = token
            });
        }

        public async Task<T> DeleteAsync<T>(int id, string token)
        {
            return await SendAsync<T>(new ApiRequest
            {
                Method = SD.ApiMethod.POST,
                Data = id,
                Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCart",
                AccessToken = token
            });
        }
    }
}
