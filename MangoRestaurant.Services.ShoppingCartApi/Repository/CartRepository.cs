using AutoMapper;
using MangoRestaurant.Services.ShoppingCartApi.DbContexts;
using MangoRestaurant.Services.ShoppingCartApi.Models;
using MangoRestaurant.Services.ShoppingCartApi.Models.Dto;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MangoRestaurant.Services.ShoppingCartApi.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CartRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CartDto> GetByUserIdAsync(string userId)
        {
            var cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);

            Cart cart = new()
            {
                CartHeader = cartHeader,
                CartDetails = await _context.CartDetails
                                .Where(c => c.CartHeaderId == cartHeader.Id)
                                .Include(c => c.Product)
                                .ToListAsync()
            };

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> CreateUpdateAsync(CartDto cartDto)
        {
            var cart = _mapper.Map<Cart>(cartDto);

            var productFromDb = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == cart.CartDetails.FirstOrDefault().ProductId);

            if (productFromDb == null)
            {
                await _context.Products.AddAsync(cart.CartDetails.FirstOrDefault().Product);
                await _context.SaveChangesAsync();
            }

            var cartHeaderFromDb = await _context.CartHeaders
                .AsNoTracking()
                .Where(c => c.UserId == cart.CartHeader.UserId)
                .FirstOrDefaultAsync();

            if(cartHeaderFromDb == null)
            {
                await _context.CartHeaders.AddAsync(cart.CartHeader);
                await _context.SaveChangesAsync();

                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;

                await _context.CartDetails.AddAsync(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else
            {
                var cartDetailsFromDb = await _context.CartDetails
                    .AsNoTracking()
                    .Where(c => c.ProductId == cart.CartDetails.FirstOrDefault().ProductId)
                    .Where(c => c.CartHeaderId == cartHeaderFromDb.Id)
                    .FirstOrDefaultAsync();

                if(cartDetailsFromDb == null)
                {
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeaderFromDb.Id;
                    cart.CartDetails.FirstOrDefault().Product = null;

                    await _context.CartDetails.AddAsync(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
                else
                {
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetailsFromDb.Count;

                    _context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
            }

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<bool> RemoveFromCartAsync(int cartDetailsId)
        {
            var cartDetails = await _context.CartDetails.FirstOrDefaultAsync(c => c.Id == cartDetailsId);

            int totalCount = await _context.CartDetails.Where(c => c.CartHeaderId == cartDetails.CartHeaderId).CountAsync();

            _context.CartDetails.Remove(cartDetails);

            if(totalCount == 1)
            {
                var cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.Id == cartDetails.CartHeaderId);

                _context.CartHeaders.Remove(cartHeader);
                await _context.SaveChangesAsync();

                return true;
            }

            await _context.SaveChangesAsync();

            return false;
        }

        public async Task<bool> ClearAsync(string userId)
        {
            var cartHeaderFromDb = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);

            if(cartHeaderFromDb != null)
            {
                var cartDetailsFromDb = await _context.CartDetails
                    .Where(c => c.CartHeaderId == cartHeaderFromDb.Id)
                    .ToListAsync();

                _context.CartDetails.RemoveRange(cartDetailsFromDb);
                _context.CartHeaders.Remove(cartHeaderFromDb);

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> ApplyCouponAsync(string userId, string couponCode)
        {
            var cart = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);

            cart.CouponCode = couponCode;
            _context.CartHeaders.Update(cart);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveCouponAsync(string userId)
        {
            var cart = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);

            cart.CouponCode = "";
            _context.CartHeaders.Update(cart);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
