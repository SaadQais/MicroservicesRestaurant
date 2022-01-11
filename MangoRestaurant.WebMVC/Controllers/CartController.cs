#nullable disable

using MangoRestaurant.WebMVC.Models;
using MangoRestaurant.WebMVC.Models.ShoppingCart;
using MangoRestaurant.WebMVC.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MangoRestaurant.WebMVC.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public CartController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetCartByUserAsycnAsync());
        }

        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            string userId = User.Claims.Where(u => u.Type == "sub").FirstOrDefault().Value;
            string accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _cartService.DeleteAsync<ResponseDto>(cartDetailsId, accessToken);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        private async Task<CartDto> GetCartByUserAsycnAsync()
        {
            string userId = User.Claims.Where(u => u.Type == "sub").FirstOrDefault().Value;
            string accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _cartService.GetByUserIdAsync<ResponseDto>(userId, accessToken);

            CartDto cartDto = new();

            if(response != null && response.IsSuccess)
            {
                cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
            }

            if(cartDto.CartHeader != null)
            {
                foreach(var details in cartDto.CartDetails)
                {
                    cartDto.CartHeader.OrderTotal += (details.Product.Price * details.Count);
                }
            }

            return cartDto;
        }
    }
}
