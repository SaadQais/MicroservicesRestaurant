using AutoMapper;
using MangoRestaurant.Services.CouponApi.DbContexts;
using MangoRestaurant.Services.CouponApi.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace MangoRestaurant.Services.CouponApi.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CouponRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CouponDto> GetByCodeAsync(string code)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Code == code);

            return _mapper.Map<CouponDto>(coupon);
        }
    }
}
