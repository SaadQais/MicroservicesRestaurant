using MangoRestaurant.Services.ProductApi.Models.Dto;

namespace MangoRestaurant.Services.ProductApi.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task<ProductDto> CreateUpdateAsync(ProductDto productDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> AnyAsync(int id);
    }
}
