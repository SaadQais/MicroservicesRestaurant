using MangoRestaurant.WebMVC.Models;
using MangoRestaurant.WebMVC.Models.Product;
using MangoRestaurant.WebMVC.Models.ShoppingCart;
using MangoRestaurant.WebMVC.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MangoRestaurant.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto>? products = new();

            var response = await _productService.GetAllAsync<ResponseDto>("");

            if(response != null && response.IsSuccess)
            {
                products = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }

            return View(products);
        }

        [Authorize]
        public async Task<IActionResult> Details(int productId)
        {
            ProductDto? product = new();

            var response = await _productService.GetByIdAsync<ResponseDto>(productId, "");

            if (response != null && response.IsSuccess)
            {
                product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }

            return View(product);
        }

        [HttpPost]
        [ActionName("Details")]
        [Authorize]
        public async Task<IActionResult> DetailsPost(ProductDto productDto)
        {
            CartDetailsDto cartDetailsDto = new()
            {
                Count = productDto.Count,
                ProductId = productDto.Id
            };

            var response = await _productService.GetByIdAsync<ResponseDto>(productDto.Id, "");

            if(response != null && response.IsSuccess)
            {
                cartDetailsDto.Product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }

            CartDto cartDto = new()
            {
                CartHeader = new CartHeaderDto
                {
                    UserId = User.Claims.Where(c => c.Type == "sub")?.FirstOrDefault()?.Value
                },

                CartDetails = new List<CartDetailsDto>() { cartDetailsDto }
            };

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var addToCartResponse = await _cartService.CreateAsync<ResponseDto>(cartDto, accessToken);

            if (addToCartResponse != null && addToCartResponse.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(productDto);
        }

        [Authorize]
        public IActionResult Login()
        {
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        public IActionResult AccessDenied()
        {
            return Redirect("https://localhost:44365/Account/AccessDenied");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}