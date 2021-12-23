using MangoRestaurant.Services.ShoppingCartApi.Models.Dto;
using MangoRestaurant.Services.ShoppingCartApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MangoRestaurant.Services.ShoppingCartApi.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;

        private readonly ResponseDto _response;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            _response = new ResponseDto();
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<object> GetCartAsync(string userId)
        {
            try
            {
                _response.Result = await _cartRepository.GetByUserIdAsync(userId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }

            return _response;
        }

        [HttpPost("AddCart")]
        public async Task<object> AddCartAsync(CartDto cartDto)
        {
            try
            {
                _response.Result = await _cartRepository.CreateUpdateAsync(cartDto);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }

            return _response;
        }

        [HttpPost("UpdateCart")]
        public async Task<object> UpdateCartAsync(CartDto cartDto)
        {
            try
            {
                _response.Result = await _cartRepository.CreateUpdateAsync(cartDto);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }

            return _response;
        }

        [HttpPost("ClearCart")]
        public async Task<object> ClearCartAsync([FromBody] string userId)
        {
            try
            {
                _response.Result = await _cartRepository.ClearAsync(userId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }

            return _response;
        }

        [HttpPost("RemoveCart")]
        public async Task<object> RemoveCartAsync([FromBody] int cartId)
        {
            try
            {
                _response.Result = await _cartRepository.RemoveFromCartAsync(cartId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }

            return _response;
        }
    }
}
