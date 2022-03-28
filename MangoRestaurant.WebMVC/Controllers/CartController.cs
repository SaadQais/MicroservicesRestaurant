#nullable disable

using MangoRestaurant.WebMVC.Models;
using MangoRestaurant.WebMVC.Models.Coupon;
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
        private readonly ICouponService _couponService;

        public CartController(IProductService productService, ICartService cartService, ICouponService couponService)
        {
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await GetCartByUserAsycnAsync());
        }

        [HttpGet]
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

        [HttpPost("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            string userId = User.Claims.Where(u => u.Type == "sub").FirstOrDefault().Value;
            string accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _cartService.ApplyCouponAsync<ResponseDto>(cartDto, accessToken);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            string userId = User.Claims.Where(u => u.Type == "sub").FirstOrDefault().Value;
            string accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _cartService.RemoveCouponAsync<ResponseDto>(cartDto.CartHeader.UserId, accessToken);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet("Checkout")]
        public async Task<IActionResult> Checkout()
        {
            return View(await GetCartByUserAsycnAsync());
        }

        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout(CartDto cartDto)
        {
            try
            {
                string accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _cartService.CheckoutAsync<ResponseDto>(cartDto.CartHeader, accessToken);

                return RedirectToAction(nameof(Confirmation));
            }
            catch
            {
                return View(cartDto);
            }
        }

        public IActionResult Confirmation()
        {
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
                if(!string.IsNullOrEmpty(cartDto.CartHeader.CouponCode))
                {
                    var couponResponse = await _couponService.GetByCodeAsync<ResponseDto>(cartDto.CartHeader.CouponCode, accessToken);

                    if (couponResponse != null && couponResponse.IsSuccess)
                    {
                        var coupon = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(couponResponse.Result));

                        cartDto.CartHeader.DiscountTotal = coupon.DiscountAmount;
                    }
                }

                foreach(var details in cartDto.CartDetails)
                {
                    cartDto.CartHeader.OrderTotal += (details.Product.Price * details.Count);
                }

                cartDto.CartHeader.OrderTotal -= cartDto.CartHeader.DiscountTotal;
            }

            return cartDto;
        }
    }
}
