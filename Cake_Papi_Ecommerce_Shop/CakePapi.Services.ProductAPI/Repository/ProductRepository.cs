using AutoMapper;
using CakePapi.Services.ProductAPI.DbContexts;
using CakePapi.Services.ProductAPI.Models;
using CakePapi.Services.ProductAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace CakePapi.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;  // Non-nullable _db
        private IMapper _mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));  // Ensure _db is non-null
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));  // Ensure mapper is non-null
        }

        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            Product product = _mapper.Map<ProductDto, Product>(productDto);

            if (product.ProductId > 0)
            {
                _db.Products.Update(product);
            }
            else
            {
                _db.Products.Add(product);
            }

            await _db.SaveChangesAsync();
            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                // No need for null check since _db is guaranteed to be non-null
                Product product = await _db.Products.FirstOrDefaultAsync(u => u.ProductId == productId);

                // If product is not found, return false
                if (product == null)
                {
                    return false;
                }

                // Remove the product from the database
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            // Retrieve the product or throw an exception if not found
            Product product = await _db.Products.Where(x => x.ProductId == productId).FirstOrDefaultAsync();

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {productId} not found.");
            }

            // Map the product to ProductDto
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            // No need for null check since _db is guaranteed to be non-null
            List<Product> productList = await _db.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(productList);
        }
    }
}
