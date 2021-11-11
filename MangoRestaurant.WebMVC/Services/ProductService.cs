using MangoRestaurant.WebMVC.Models;
using MangoRestaurant.WebMVC.Services.Interfaces;

namespace MangoRestaurant.WebMVC.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IHttpClientFactory httpClient) : base(httpClient)
        {
        }

        public async Task<T> CreateProductAsync<T>(ProductDto productDto)
        {
            return await SendAsync<T>(new ApiRequest
            {
                Method = SD.ApiMethod.POST,
                Data = productDto,
                Url = SD.ProductAPIBase + "/api/products/",
                AccessToken = ""
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id)
        {
            return await SendAsync<T>(new ApiRequest
            {
                Method = SD.ApiMethod.DELETE,
                Url = SD.ProductAPIBase + $"/api/products/{id}",
                AccessToken = ""
            });
        }

        public async Task<T> GetAllProductsAsync<T>()
        {
            return await SendAsync<T>(new ApiRequest
            {
                Method = SD.ApiMethod.GET,
                Url = SD.ProductAPIBase + $"/api/products/",
                AccessToken = ""
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id)
        {
            return await SendAsync<T>(new ApiRequest
            {
                Method = SD.ApiMethod.GET,
                Url = SD.ProductAPIBase + $"/api/products/{id}",
                AccessToken = ""
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto productDto)
        {
            return await SendAsync<T>(new ApiRequest
            {
                Method = SD.ApiMethod.PUT,
                Data = productDto,
                Url = SD.ProductAPIBase + "/api/products/",
                AccessToken = ""
            });
        }
    }
}
