using MangoRestaurant.Services.CouponApi.Models.Dto;
using MangoRestaurant.Services.CouponApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MangoRestaurant.Services.CouponApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly ICouponRepository _couponService;

        private readonly ResponseDto _response;

        public CouponsController(ICouponRepository couponService)
        {
            _couponService = couponService;
            _response = new ResponseDto();
        }

        [HttpGet("GetCoupon/{code}")]
        public async Task<object> GetCouponByCodeAsync(string code)
        {
            try
            {
                _response.Result = await _couponService.GetByCodeAsync(code);
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
