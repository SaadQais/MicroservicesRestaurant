using MangoRestaurant.WebMVC.Models.Product;

namespace MangoRestaurant.WebMVC.Services.Interfaces
{
    public interface IProductService : IBaseService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetByIdAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(ProductDto productDto, string token);
        Task<T> UpdateAsync<T>(ProductDto productDto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
