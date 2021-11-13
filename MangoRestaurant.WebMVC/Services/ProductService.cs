using MangoRestaurant.WebMVC.Models;
using MangoRestaurant.WebMVC.Services.Interfaces;

namespace MangoRestaurant.WebMVC.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IHttpClientFactory httpClient) : base(httpClient)
        {
        }

        public async Task<T> GetAllProductsAsync<T>(string token)
        {
            return await SendAsync<T>(new ApiRequest
            {
                Method = SD.ApiMethod.GET,
                Url = SD.ProductAPIBase + $"/api/products/",
                AccessToken = token
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id, string token)
        {
            return await SendAsync<T>(new ApiRequest
            {
                Method = SD.ApiMethod.GET,
                Url = SD.ProductAPIBase + $"/api/products/{id}",
                AccessToken = token
            });
        }

        public async Task<T> CreateProductAsync<T>(ProductDto productDto, string token)
        {
            return await SendAsync<T>(new ApiRequest
            {
                Method = SD.ApiMethod.POST,
                Data = productDto,
                Url = SD.ProductAPIBase + "/api/products/",
                AccessToken = token
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id, string token)
        {
            return await SendAsync<T>(new ApiRequest
            {
                Method = SD.ApiMethod.DELETE,
                Url = SD.ProductAPIBase + $"/api/products/{id}",
                AccessToken = token
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto productDto, string token)
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
