using AutoMapper;
using AutoMapper.QueryableExtensions;
using MangoRestaurant.AutoMapper;
using MangoRestaurant.Services.ProductApi.DbContexts;
using MangoRestaurant.Services.ProductApi.Models;
using MangoRestaurant.Services.ProductApi.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace MangoRestaurant.Services.ProductApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            return await _context.Products
                .ProjectTo<ProductDto>(AutoMapperConfiguration.RegisterMappings())
                .ToListAsync();
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            return await _context.Products
                .ProjectTo<ProductDto>(AutoMapperConfiguration.RegisterMappings())
                .FirstAsync(p => p.Id == id);
        }

        public async Task<ProductDto> CreateUpdateAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            if(product.Id > 0)
                _context.Update(product);
            else
                await _context.AddAsync(product);

            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if(product == null)
                return false;

            _context.Remove(product);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }
    }
}
