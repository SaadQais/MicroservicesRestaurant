using MangoRestaurant.Services.ShoppingCartApi.Models.Dto;

namespace MangoRestaurant.Services.ShoppingCartApi.Repository
{
    public interface ICartRepository
    {
        Task<CartDto> GetByUserIdAsync(string userId);
        Task<CartDto> CreateUpdateAsync(CartDto cartDto);
        Task<bool> RemoveFromCartAsync(int cartDetailsId);
        Task<bool> ClearAsync(string userId);
    }
}
