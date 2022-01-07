using MangoRestaurant.WebMVC.Models;
using MangoRestaurant.WebMVC.Models.Product;
using MangoRestaurant.WebMVC.Services.Interfaces;

namespace MangoRestaurant.WebMVC.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IHttpClientFactory _httpClient;

        public ProductService(IHttpClientFactory httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAllAsync<T>(string token)
        {
            return await SendAsync<T>(new ApiRequest
            {
                Method = SD.ApiMethod.GET,
                Url = SD.ProductAPIBase + $"/api/products/",
                AccessToken = token
            });
        }

        public async Task<T> GetByIdAsync<T>(int id, string token)
        {
            return await SendAsync<T>(new ApiRequest
            {
                Method = SD.ApiMethod.GET,
                Url = SD.ProductAPIBase + $"/api/products/{id}",
                AccessToken = token
            });
        }

        public async Task<T> CreateAsync<T>(ProductDto productDto, string token)
        {
            return await SendAsync<T>(new ApiRequest
            {
                Method = SD.ApiMethod.POST,
                Data = productDto,
                Url = SD.ProductAPIBase + "/api/products/",
                AccessToken = token
            });
        }

        public async Task<T> DeleteAsync<T>(int id, string token)
        {
            return await SendAsync<T>(new ApiRequest
            {
                Method = SD.ApiMethod.DELETE,
                Url = SD.ProductAPIBase + $"/api/products/{id}",
                AccessToken = token
            });
        }

        public async Task<T> UpdateAsync<T>(ProductDto productDto, string token)
        {
            return await SendAsync<T>(new ApiRequest
            {
                Method = SD.ApiMethod.PUT,
                Data = productDto,
                Url = SD.ProductAPIBase + "/api/products/",
                AccessToken = token
            });
        }
    }
}
