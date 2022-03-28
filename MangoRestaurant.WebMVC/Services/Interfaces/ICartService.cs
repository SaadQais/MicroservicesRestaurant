using MangoRestaurant.WebMVC.Models.ShoppingCart;

namespace MangoRestaurant.WebMVC.Services.Interfaces
{
    public interface ICartService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetByUserIdAsync<T>(string userId, string token);
        Task<T> CreateAsync<T>(CartDto cartDto, string token);
        Task<T> UpdateAsync<T>(CartDto cartDto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
        Task<T> ApplyCouponAsync<T>(CartDto cartDto, string token);
        Task<T> RemoveCouponAsync<T>(string userId, string token);
        Task<T> CheckoutAsync<T>(CartHeaderDto cartHeader, string token);
    }
}
